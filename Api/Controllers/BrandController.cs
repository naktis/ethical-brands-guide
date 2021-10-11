using Api.Validators;
using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IBrandProvider _provider;
        private readonly ICompanyProvider _companyProvider;
        private readonly ICategoryProvider _categoryProvider;
        private readonly IValidator _validator;

        public BrandController(ILogger<CategoryController> logger, 
            IBrandProvider provider, IValidator validator, 
            ICompanyProvider companyProvider, ICategoryProvider categoryProvider)
        {
            _logger = logger;
            _provider = provider;
            _companyProvider = companyProvider;
            _validator = validator;
            _categoryProvider = categoryProvider;
        }


        [HttpGet("{key}")]
        public async Task<ActionResult<CategoryOutDto>> GetBrand(int key)
        {
            if(_validator.KeyNegative(key))
                return BadRequest();

            if (!await _provider.KeyExists(key))
                return NotFound();

            return Ok(await _provider.Get(key));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandOutMultiDto>>> GetBrands(
            string query, string sortType = "any", int categoryId = 0)
        {
            if (_validator.KeyNegative(categoryId) || 
                _validator.QueryTooLong(query) ||
                _validator.SortTypeInvalid(sortType))
                return BadRequest();

            return Ok(await _provider.Get(query, sortType, categoryId));
        }

        [HttpGet("Count")]
        public async Task<ActionResult<CategoryOutDto>> GetBrand()
        {
            return Ok(await _provider.Count());
        }

        [HttpPost(Name = nameof(PostBrand))]
        public async Task<ActionResult<BrandOutPostDto>> PostBrand(BrandInDto brand)
        {
            var createdBrand = await _provider.Add(brand);

            _logger.LogInformation($"New brand (id={createdBrand.BrandId}) has been added");
            return CreatedAtRoute(nameof(PostBrand), createdBrand);
        }

        [HttpPut("{key}")]
        public async Task<ActionResult<BrandOutDto>> UpdateBrand([FromRoute] int key, [FromBody] BrandInDto newBrand)
        {
            if (_validator.KeyNegative(key))
                return BadRequest();

            if (!await _provider.KeyExists(key))
                return NotFound();

            await _provider.Update(key, newBrand);

            _logger.LogInformation($"Brand with id={key} has been updated");
            return Ok();
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteBrand(int key)
        {
            if (_validator.KeyNegative(key))
                return BadRequest();

            if (!await _provider.KeyExists(key))
                return NotFound();

            await _provider.Delete(key);

            _logger.LogInformation($"Brand with id={key} has been deleted");
            return Ok();
        }
    }
}
