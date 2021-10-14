using Api.Validators;
using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Services.Interfaces;
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
        private readonly IValidator _validator;

        public CompanyController(ILogger<CategoryController> logger, 
            ICompanyProvider provider, IValidator validator)
        {
            _logger = logger;
            _provider = provider;
            _validator = validator;
        }


        [HttpGet("{key}")]
        public async Task<ActionResult<CompanyOutDto>> GetCompany(int key)
        {
            if (!await _provider.KeyExists(key))
                return NotFound();

            return Ok(await _provider.Get(key));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyOutDto>>> GetCompanies()
        {
            return Ok(await _provider.GetAll());
        }

        [HttpPost(Name = nameof(PostCompany))]
        public async Task<ActionResult<CompanyOutDto>> PostCompany(CompanyInDto company)
        {
            if (await _provider.Exists(company))
                return BadRequest();

            var createdCompany = await _provider.Add(company);

            _logger.LogInformation($"New company (id={createdCompany.CompanyId}) has been added");
            return CreatedAtRoute(nameof(PostCompany), createdCompany);
        }

        [HttpPut("{key}")]
        public async Task<ActionResult<CompanyOutDto>> UpdateCompany([FromRoute] int key, [FromBody] CompanyInDto newCompany)
        {
            if (!await _provider.KeyExists(key))
                return NotFound();

            await _provider.Update(key, newCompany);

            _logger.LogInformation($"Company with id={key} has been updated");
            return Ok();
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteCompany(int key)
        {
            if (!await _provider.KeyExists(key))
                return NotFound();

            await _provider.Delete(key);

            _logger.LogInformation($"Company with id={key} has been deleted");
            return Ok();
        }
    }
}
