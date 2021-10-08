using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Services;
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

        public BrandController(ILogger<CategoryController> logger, IBrandProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }


        [HttpGet("{key}")]
        public async Task<ActionResult<CategoryOutDto>> GetBrand(int key)
        {
            if (!await _provider.KeyExists(key))
                return NotFound();

            return Ok(await _provider.Get(key));
        }

        [HttpGet]
        public ActionResult<IEnumerable<LightBrandOutDto>> GetBrands(string query)
        {
            return Ok(_provider.Get(query));
        }

        [HttpPost(Name = nameof(PostBrand))]
        public async Task<ActionResult<LightBrandOutDto>> PostBrand(BrandInDto brand)
        {
            var createdBrand = await _provider.Add(brand);

            return CreatedAtRoute(nameof(PostBrand), createdBrand);
        }

        [HttpPut("{key}")]
        public async Task<ActionResult<BrandOutDto>> UpdateBrand([FromRoute] int key, [FromBody] BrandInDto newBrand)
        {
            if (!await _provider.KeyExists(key))
                return NotFound();

            await _provider.Update(key, newBrand);

            return Ok();
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteBrand(int key)
        {
            if (!await _provider.KeyExists(key))
                return NotFound();

            await _provider.Delete(key);

            return Ok();
        }
    }
}
