using Business.Dto.InputDto;
using Business.Dto.OutputDto;
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
    public class BrandProvider : IBrandProvider
    {
        private readonly AppDbContext _context;
        private readonly IBrandMapper _mapper;
        private readonly IBrandCategoryProvider _bcProvider;

        public BrandProvider(AppDbContext context, IBrandMapper mapper, 
            IBrandCategoryProvider bcProvider)
        {
            _context = context;
            _mapper = mapper;
            _bcProvider = bcProvider;
        }

        public async Task<BrandOutPostDto> Add(BrandInDto dto)
        {
            var entity = _mapper.EntityFromDto(dto);

            var company = await _context.Companies.FindAsync(dto.CompanyId);
            entity.Company = company;

            var createdBrand = await _context.Brands.AddAsync(entity);
            company.Brands.Add(createdBrand.Entity);
            await _context.SaveChangesAsync();

            if (dto.CategoryIds.Any())
                await _bcProvider.AddRange(dto.CategoryIds, createdBrand.Entity);
            
            return _mapper.EntityToPostDto(createdBrand.Entity);
        }
        
        public async Task Delete(int key)
        {
            var brand = await _context.Brands.FindAsync(key);
            var brandCategories = await _context.BrandsCategories.Where(x => x.BrandId == key).ToListAsync();

            _context.BrandsCategories.RemoveRange(brandCategories);
            _context.Brands.Remove(brand);

            await _context.SaveChangesAsync();
        }
        
        public async Task Update(int key, BrandInDto newBrand)
        {
            var brand = await _context.Brands.FindAsync(key);
            brand = _mapper.CopyFromDto(brand, newBrand);
            brand.Company = await _context.Companies.FindAsync(brand.CompanyId);
            await _context.SaveChangesAsync();

            await _bcProvider.RemoveRangeByBrand(key);
            
            if (newBrand.CategoryIds.Any())
                await _bcProvider.AddRange(newBrand.CategoryIds, brand);
        }
        
        public async Task<BrandOutDto> Get(int key)
        {
            var brand = await _context.Brands
                .Where(b => b.BrandId == key)
                .Include(b => b.Company)
                .ThenInclude(c => c.Rating)
                .Include(b => b.BrandsCategories)
                .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync();

            return _mapper.EntityToDto(brand);
        }

        public async Task<IEnumerable<BrandOutMultiDto>> Get(string query, string sortType = "any", int categoryId = 0)
        {
            var brands = new List<Brand>();

            if (query == null && categoryId == 0)
                brands = await _context.Brands
                    .Include(b => b.Company)
                    .ThenInclude(c => c.Rating)
                    .ToListAsync();
            else if (query != null && categoryId == 0)
                brands = await _context.Brands
                    .Where(b => b.Name.Contains(query))
                    .Include(b => b.Company)
                    .ThenInclude(c => c.Rating)
                    .ToListAsync();
            else if (query == null && categoryId != 0)
                brands = await _context.Brands
                    .Where(b => b.BrandsCategories
                        .Any(c => c.Category.CategoryId == categoryId))
                    .Include(b => b.Company)
                    .ThenInclude(c => c.Rating)
                    .ToListAsync();
            else // query != null && categoryId != null
                brands = await _context.Brands.Where(
                    b => b.BrandsCategories
                    .Any(c => c.Category.CategoryId == categoryId) && b.Name.Contains(query))
                    .Include(b => b.Company)
                    .ThenInclude(c => c.Rating)
                    .ToListAsync();

            return Sort(_mapper.EntitiesToDtos(brands), sortType);
        }

        public async Task<bool> KeyExists(int key)
        {
            return await _context.Brands.FindAsync(key) != null;
        }

        public async Task<int> Count()
        {
            return await _context.Brands.CountAsync();
        }

        public async Task RemoveRangeByCompany(int companyId)
        {
            var brands = await _context.Brands.Where(b => b.CompanyId == companyId).ToListAsync();

            foreach (var b in brands)
                await _bcProvider.RemoveRangeByBrand(b.BrandId);

            _context.Brands.RemoveRange(brands);

            await _context.SaveChangesAsync();
        }

        private IEnumerable<BrandOutMultiDto> Sort(IList<BrandOutMultiDto> brands, string sortType)
        {
            var sortOptions = new Dictionary<string, IList<BrandOutMultiDto>>()
            {
                {"any", brands},
                {"total", brands.OrderByDescending(b => b.RatingTotal).ToList()},
                {"planet", brands.OrderByDescending(b => b.RatingPlanet).ToList()},
                {"people", brands.OrderByDescending(b => b.RatingPeople).ToList()},
                {"animals", brands.OrderByDescending(b => b.RatingAnimals).ToList()}
            };

            return sortOptions[sortType];
        }
    }
}
