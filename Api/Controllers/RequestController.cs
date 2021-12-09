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

        public RequestController(ILogger<RequestController> logger, IRequestProvider provider)
        {
            _logger = logger;
            _provider = provider;
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
    }
}
