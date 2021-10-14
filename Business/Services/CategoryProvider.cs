using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Mappers.Interfaces;
using Business.Services.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CategoryProvider : ICategoryProvider
    {
        private readonly AppDbContext _context;
        private readonly ICategoryMapper _mapper;
        private readonly IBrandCategoryProvider _bcProvider;
        public CategoryProvider(AppDbContext context, ICategoryMapper mapper,
            IBrandCategoryProvider bcProvider)
        {
            _context = context;
            _mapper = mapper;
            _bcProvider = bcProvider;
        }

        public async Task<CategoryOutDto> Add(CategoryInDto category)
        {
            var createdCategory = await _context.Categories
                .AddAsync(_mapper.EntityFromDto(category));

            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(createdCategory.Entity);
        }

        public async Task Delete(int key)
        {
            await _bcProvider.RemoveRangeByCategory(key);

            var category = await _context.Categories.FindAsync(key);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int key, CategoryInDto newCategory)
        {
            var oldCategory = await _context.Categories.FindAsync(key);
            oldCategory = _mapper.CopyFromDto(oldCategory, newCategory); // not unnecessary assignment
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryOutDto> Get(int key)
        {
            return _mapper.EntityToDto(await _context.Categories.FindAsync(key));
        }

        public async Task<IEnumerable<CategoryOutDto>> GetAll()
        {
            return _mapper.EntitiesToDtos(await _context.Categories.ToListAsync());
        }

        public async Task<bool> KeyExists(int key)
        {
            return await _context.Categories.FindAsync(key) != null;
        }

        public async Task<bool> Exists(CategoryInDto category)
        {
            return await _context.Categories.AnyAsync(c => c.Name == category.Name);
        }

        public async Task<bool> AllExist(List<int> categoryIds)
        {
            bool allExist = true;

            foreach (var id in categoryIds)
                if (!await KeyExists(id))
                {
                    allExist = false;
                    break;
                }

            return allExist;
        }
    }
}
