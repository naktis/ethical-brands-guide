using Api.RequestProcessors.Validators.Interfaces;
using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICompanyProvider _provider;
        private readonly IKeyValidator _keyValidator;

        public CompanyController(ILogger<CategoryController> logger, 
            ICompanyProvider provider, IKeyValidator keyValidator)
        {
            _logger = logger;
            _provider = provider;
            _keyValidator = keyValidator;
        }


        [HttpGet("{key}")]
        [AllowAnonymous]
        public async Task<ActionResult<CompanyOutDto>> GetCompany(int key)
        {
            if (!_keyValidator.Validate(key))
                return BadRequest();

            if (!await _provider.KeyExists(key))
                return NotFound();

            return Ok(await _provider.Get(key));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CompanyOutDto>>> GetCompanies()
        {
            return Ok(await _provider.GetAll());
        }

        [HttpPost(Name = nameof(PostCompany))]
        [Authorize]
        public async Task<ActionResult<CompanyOutDto>> PostCompany(CompanyInDto company)
        {
            if (await _provider.Exists(company))
                return BadRequest();

            var createdCompany = await _provider.Add(company);

            _logger.LogInformation($"New company (id={createdCompany.CompanyId}) has been added");
            return CreatedAtRoute(nameof(PostCompany), createdCompany);
        }

        [HttpPut("{key}")]
        [Authorize]
        public async Task<ActionResult<CompanyOutDto>> UpdateCompany([FromRoute] int key, [FromBody] CompanyInDto newCompany)
        {
            if (!_keyValidator.Validate(key))
                return BadRequest();

            if (!await _provider.KeyExists(key))
                return NotFound();

            await _provider.Update(key, newCompany);

            _logger.LogInformation($"Company with id={key} has been updated");
            return Ok();
        }

        [HttpDelete("{key}")]
        [Authorize]
        public async Task<IActionResult> DeleteCompany(int key)
        {
            if (!_keyValidator.Validate(key))
                return BadRequest();

            if (!await _provider.KeyExists(key))
                return NotFound();

            await _provider.Delete(key);

            _logger.LogInformation($"Company with id={key} has been deleted");
            return Ok();
        }
    }
}
