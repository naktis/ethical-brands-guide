using Api.RequestProcessors.TokenExtractors;
using Api.RequestProcessors.Validators.Interfaces;
using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Services.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUserProvider _provider;
        private readonly IClaimExtractor _extractor;
        private readonly IUserValidator _validator;

        public UserController(ILogger<CategoryController> logger, IUserProvider provider,
            IClaimExtractor extractor, IUserValidator validator)
        {
            _logger = logger;
            _provider = provider;
            _extractor = extractor;
            _validator = validator;
        }

        [HttpPost(Name = nameof(PostUser))]
        [Authorize]
        public async Task<ActionResult<UserOutDto>> PostUser(UserInDto user)
        {
            var role = _extractor.GetRole(HttpContext.User.Identity as ClaimsIdentity);
            if (role != UserType.Admin.ToString())
                return Forbid();

            if (!_validator.Validate(user) || await _provider.Exists(user))
                return BadRequest();

            var createdUser = await _provider.Add(user);

            _logger.LogInformation($"New user (id={createdUser.UserId}) has been added");
            return CreatedAtRoute(nameof(PostUser), createdUser);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResultDto>> Login([FromBody] LoginDto request)
        {
            if (!_validator.ValidateLogin(request))
                return BadRequest();

            if (!await _provider.UsernameMatchesPass(request))
            {
                _logger.LogInformation($"Log in denied for username {request.Username}");
                return NotFound();
            }

            var result = await _provider.Authenticate(request);
            _logger.LogInformation($"Log in accepted for user with id = {result.UserId}");
            return Ok(result);
        }
    }
}
