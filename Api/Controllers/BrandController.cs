using Api.Validators.Interfaces;
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
    public class BrandController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IBrandProvider _provider;
        private readonly ICompanyProvider _companyProvider;
        private readonly ICategoryProvider _categoryProvider;
        private readonly IBrandParamsValidator _bpValidator;
        private readonly IKeyValidator _keyValidator;
        private readonly INewBrandValidator _brandValidator;

        public BrandController(ILogger<CategoryController> logger, 
            IBrandProvider provider, IBrandParamsValidator bpValidator, 
            ICompanyProvider companyProvider, ICategoryProvider categoryProvider,
            IKeyValidator keyValidator, INewBrandValidator brandValidator)
        {
            _logger = logger;
            _provider = provider;
            _companyProvider = companyProvider;
            _bpValidator = bpValidator;
            _categoryProvider = categoryProvider;
            _keyValidator = keyValidator;
            _brandValidator = brandValidator;
        }


        [HttpGet("{key}")]
        public async Task<ActionResult<CategoryOutDto>> GetBrand(int key)
        {
            if(!_keyValidator.Validate(key))
                return BadRequest();

            if (!await _provider.KeyExists(key))
                return NotFound();

            return Ok(await _provider.Get(key));
        }

        [HttpGet]
        public ActionResult<IEnumerable<BrandOutMultiDto>> GetBrands(
            [FromQuery] BrandParametersDto brandParams)
        {
            if (!_bpValidator.Validate(brandParams))
                return BadRequest();

            return Ok(_provider.Get(brandParams));
        }

        [HttpGet("Count")]
        public async Task<ActionResult<CategoryOutDto>> GetBrand()
        {
            return Ok(await _provider.Count());
        }

        [HttpPost(Name = nameof(PostBrand))]
        public async Task<ActionResult<BrandOutPostDto>> PostBrand(BrandInDto brand)
        {
            if (!_brandValidator.Validate(brand))
                return BadRequest();

            var createdBrand = await _provider.Add(brand);

            _logger.LogInformation($"New brand (id={createdBrand.BrandId}) has been added");
            return CreatedAtRoute(nameof(PostBrand), createdBrand);
        }

        [HttpPut("{key}")]
        public async Task<ActionResult<BrandOutDto>> UpdateBrand([FromRoute] int key, [FromBody] BrandInDto newBrand)
        {
            if (!_keyValidator.Validate(key))
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
            if (!_keyValidator.Validate(key))
                return BadRequest();

            if (!await _provider.KeyExists(key))
                return NotFound();

            await _provider.Delete(key);

            _logger.LogInformation($"Brand with id={key} has been deleted");
            return Ok();
        }
    }
}
