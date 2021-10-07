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

        public async Task Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(CategoryInDto category)
        {
            _context.Update(_mapper.CategoryFromDto(category));
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryOutDto> Get(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return _mapper.CategoryToDto(category);
        }

        public async Task<IEnumerable<CategoryOutDto>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.CategoryToDto(categories);
        }

        public async Task<bool> ExistsByKey(int id)
        {
            if (await _context.Categories.FindAsync(id) == null)
                return false;

            return true;
        }

        public async Task<bool> ExistsByName(string name)
        {
            if (await _context.Categories.AnyAsync(c => c.Name == name))
                return true;

            return false;
        }
    }
}
