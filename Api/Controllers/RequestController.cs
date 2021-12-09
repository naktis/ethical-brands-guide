using Api.RequestProcessors.Validators.Interfaces;
using Business.Dto.InputDto;
using Business.Dto.InputDto.RequestParameters;
using Business.Dto.OutputDto;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRequestProvider _provider;
        private readonly IKeyValidator _keyValidator;

        public RequestController(ILogger<RequestController> logger, IRequestProvider provider,
            IKeyValidator keyValidator)
        {
            _logger = logger;
            _provider = provider;
            _keyValidator = keyValidator;
        }

        [HttpPost(Name = nameof(PostRequest))]
        public async Task<ActionResult<RequestOutDto>> PostRequest(RequestInDto request)
        {
            var createdRequest = await _provider.Add(request);
            _logger.LogInformation($"New request (id={createdRequest.RequestId}) has been added");
            return CreatedAtRoute(nameof(PostRequest), createdRequest);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<RequestOutDto>> GetRequests([FromQuery]PagingParameters paging)
        {
            return Ok(_provider.GetAll(paging));
        }

        [HttpDelete("{key}")]
        [Authorize]
        public async Task<IActionResult> DeleteRequest(int key)
        {
            if (!_keyValidator.Validate(key))
                return BadRequest();

            if (!await _provider.KeyExists(key))
                return NotFound();

            await _provider.Delete(key);

            _logger.LogInformation($"Request with id={key} has been deleted");
            return Ok();
        }
    }
}
