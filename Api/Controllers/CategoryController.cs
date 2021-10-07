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
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryProvider _provider;

        public CategoryController(ILogger<CategoryController> logger, ICategoryProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }


        [HttpGet("{key}")]
        public async Task<ActionResult<CategoryOutDto>> GetCategory(int key)
        {
            if (!await _provider.KeyExists(key))
                return NotFound();

            return Ok(await _provider.Get(key));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryOutDto>>> GetCategories()
        {
            return Ok(await _provider.GetAll());
        }

        [HttpPost(Name = nameof(PostCategory))]
        public async Task<ActionResult<CategoryOutDto>> PostCategory(CategoryInDto category)
        {
            if (await _provider.Exists(category))
                return BadRequest();

            var createdCategory = await _provider.Add(category);

            return CreatedAtRoute(nameof(PostCategory), createdCategory);
        }

        [HttpPut("{key}")]
        public async Task<ActionResult<CategoryOutDto>> UpdateCategory([FromRoute] int key, [FromBody] CategoryInDto newCategory)
        {
            if (await _provider.Exists(newCategory))
                return BadRequest();

            if (!await _provider.KeyExists(key))
                return NotFound();

            await _provider.Update(key, newCategory);

            return Ok();
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteCategory(int key)
        {
            if (!await _provider.KeyExists(key))
                return NotFound();

            await _provider.Delete(key);

            return Ok();
        }
    }
}
