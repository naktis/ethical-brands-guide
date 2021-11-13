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
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUserProvider _provider;

        public UserController(ILogger<CategoryController> logger, IUserProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        [HttpPost(Name = nameof(PostUser))]
        public async Task<ActionResult<UserOutDto>> PostUser(UserInDto brand)
        {
            /* TODO: User Validation
            if (!_brandValidator.Validate(brand))
                return BadRequest();
            */

            var createdUser = await _provider.Add(brand);

            _logger.LogInformation($"New brand (id={createdUser.UserId}) has been added");
            return CreatedAtRoute(nameof(PostUser), createdUser);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResultDto>> Login([FromBody] LoginDto request)
        {
            /* TODO: Login Validation
            if (!_userValidator.ValidateLogin(request))
                return BadRequest();
            */

            if (!await _provider.EmailMatchesPass(request))
            {
                _logger.LogInformation($"Log in denied for email address {request.Email}");
                return NotFound();
            }

            var result = await _provider.Authenticate(request);
            _logger.LogInformation($"Log in accepted for user with id = {result.UserId}");
            return Ok(result);
        }
    }
}
