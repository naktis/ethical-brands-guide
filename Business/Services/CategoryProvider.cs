using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CategoryProvider : ICategoryProvider
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CategoryProvider(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryOutDto> Add(CategoryInDto category)
        {
            var createdCategory = await _context.Categories
                .AddAsync(_mapper.CategoryFromDto(category));
            await _context.SaveChangesAsync();

            return _mapper.CategoryToDto(createdCategory.Entity);
        }

        public async Task Delete(int key)
        {
            var category = await _context.Categories.FindAsync(key);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int key, CategoryInDto newCategory)
        {
            var oldCategory = await _context.Categories.FindAsync(key);
            oldCategory = _mapper.CopyFromDto(oldCategory, newCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryOutDto> Get(int key)
        {
            return _mapper.CategoryToDto(await _context.Categories.FindAsync(key));
        }

        public async Task<IEnumerable<CategoryOutDto>> GetAll()
        {
            return _mapper.CategoryToDto(await _context.Categories.ToListAsync());
        }

        public async Task<bool> KeyExists(int key)
        {
            if (await _context.Categories.FindAsync(key) == null)
                return false;

            return true;
        }

        public async Task<bool> Exists(CategoryInDto category)
        {
            if (await _context.Categories.AnyAsync(c => c.Name == category.Name))
                return true;

            return false;
        }
    }
}
