using Business.Mappers.Interfaces;
using Business.Services.Interfaces;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class BrandCategoryProvider : IBrandCategoryProvider
    {
        private readonly AppDbContext _context;
        private readonly IBrandCategoryMapper _mapper;

        public BrandCategoryProvider(AppDbContext context, IBrandCategoryMapper bcMapper)
        {
            _context = context;
            _mapper = bcMapper;
        }

        public async Task AddRange(IEnumerable<int> categoryIds, Brand brand)
        {
            foreach (var id in categoryIds)
            {
                var category = await _context.Categories.FindAsync(id);
                var brandCategory = _mapper.NewEntity(category, brand);

                var createdBrandCategory = await _context.BrandsCategories.AddAsync(brandCategory);
                brand.BrandsCategories.Add(createdBrandCategory.Entity);
                category.BrandsCategories.Add(createdBrandCategory.Entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeByCategory(int key)
        {
            var bcs = await _context.BrandsCategories.Where(x => x.CategoryId == key).ToListAsync();

            if (bcs.Any())
                _context.BrandsCategories.RemoveRange(bcs);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeByBrand(int key)
        {
            var bcs = await _context.BrandsCategories.Where(x => x.BrandId == key).ToListAsync();

            if (bcs.Any())
                _context.BrandsCategories.RemoveRange(bcs);

            await _context.SaveChangesAsync();
        }
    }
}
