using Api.RequestProcessors.Validators.Interfaces;
using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRatingProvider _provider;
        private readonly ICompanyProvider _companyProvider;
        private readonly IKeyValidator _keyValidator;

        public RatingController(ILogger<RequestController> logger, IRatingProvider provider,
            IKeyValidator keyValidator, ICompanyProvider companyProvider)
        {
            _logger = logger;
            _provider = provider;
            _keyValidator = keyValidator;
            _companyProvider = companyProvider;
        }

        [HttpPost(Name = nameof(PostRating))]
        public async Task<ActionResult> PostRating(RatingEntryInDto rating)
        {
            var createdRating = await _provider.AddAsync(rating);
            _logger.LogInformation($"New rating for company id = {rating.CompanyId} has been submitted");
            return CreatedAtRoute(nameof(PostRating), createdRating);
        }

        [HttpGet("Guest/{companyId}")]
        public async Task<ActionResult<RequestOutDto>> GetGuestRating(int companyId)
        {
            if (!_keyValidator.Validate(companyId))
                return BadRequest();

            if (!await _companyProvider.KeyExists(companyId))
                return NotFound();

            return Ok(await _provider.GetGuestRatingAsync(companyId));
        }
    }
}
