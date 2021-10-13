using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Mappers.Interfaces;
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
        private readonly ICategoryMapper _categoryMapper;
        private readonly ICompanyMapper _companyMapper;
        private readonly IBrandCategoryMapper _bcMapper;

        public BrandProvider(AppDbContext context, IBrandMapper mapper, 
            ICategoryMapper categoryMapper, ICompanyMapper companyMapper,
            IBrandCategoryMapper bcMapper)
        {
            _context = context;
            _mapper = mapper;
            _categoryMapper = categoryMapper;
            _companyMapper = companyMapper;
            _bcMapper = bcMapper;
        }

        public async Task<BrandOutPostDto> Add(BrandInDto dto)
        {
            var entity = _mapper.EntityFromDto(dto);

            var company = await _context.Companies.FindAsync(dto.CompanyId);
            entity.Company = company;

            var createdBrand = await _context.Brands.AddAsync(entity);
            company.Brands.Add(createdBrand.Entity);
            await _context.SaveChangesAsync();

            // Map brand with categories
            if(dto.CategoryIds.Any())
            {
                foreach (var categoryId in dto.CategoryIds)
                {
                    var category = await _context.Categories.FindAsync(categoryId);
                    var brandCategory = _bcMapper.NewEntity(category, createdBrand.Entity);

                    var createdBrandCategory = await _context.BrandsCategories.AddAsync(brandCategory);
                    createdBrand.Entity.BrandsCategories.Add(createdBrandCategory.Entity);
                    category.BrandsCategories.Add(createdBrandCategory.Entity);
                }
                await _context.SaveChangesAsync();
            }
            
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
            brand.Company = await _context.Companies.FindAsync(brand.CompanyId); ;

            // Remove old brandscategories
            var brandCategories = await _context.BrandsCategories.Where(x => x.BrandId == key).ToListAsync();
            if (brandCategories.Any())
                _context.BrandsCategories.RemoveRange(brandCategories);

            await _context.SaveChangesAsync();

            // Add new brandscategories
            if (newBrand.CategoryIds.Any())
            {
                foreach (var categoryId in newBrand.CategoryIds)
                {
                    var category = await _context.Categories.FindAsync(categoryId);
                    var brandCategory = _bcMapper.NewEntity(category, brand);

                    var createdBrandCategory = await _context.BrandsCategories.AddAsync(brandCategory);
                    brand.BrandsCategories.Add(createdBrandCategory.Entity);
                    category.BrandsCategories.Add(createdBrandCategory.Entity);
                }
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<BrandOutDto> Get(int key)
        {
            var brand = await _context.Brands.FindAsync(key);
            var brandCategories = await _context.BrandsCategories.Where(x => x.BrandId == key).ToListAsync();
            var company = await _context.Companies.FindAsync(brand.CompanyId);
            var rating = await _context.Ratings.FindAsync(company.RatingId);

            var brandDto = _mapper.EntityToDto(brand);
            brandDto.Company = _companyMapper.EntityToDto(company, rating);

            brandDto.Categories = new List<CategoryOutDto>();
            foreach (var bc in brandCategories)
            {
                var category = await _context.Categories.FindAsync(bc.CategoryId);
                brandDto.Categories.Add(_categoryMapper.EntityToDto(category));
            }

            return brandDto;
        }

        public async Task<IEnumerable<BrandOutMultiDto>> Get(string query, string sortType = "any", int categoryId = 0)
        {
            var brands = new List<Brand>();

            if (query == null && categoryId == 0)
                brands = await _context.Brands.ToListAsync();
            else if (query != null && categoryId == 0)
                brands = await _context.Brands.Where(b => b.Name.Contains(query)).ToListAsync();
            else if (query == null && categoryId != 0)
                brands = await _context.Brands.Where(
                    b => b.BrandsCategories.Any(c => c.Category.CategoryId == categoryId))
                    .ToListAsync();
            else // query != null && categoryId != null
                brands = await _context.Brands.Where(
                    b => b.BrandsCategories.Any(c => c.Category.CategoryId == categoryId) && 
                    b.Name.Contains(query))
                    .ToListAsync();

            var brandsDto = new List<BrandOutMultiDto>();

            foreach (var b in brands)
            {
                var dto = _mapper.EntityToMultiDto(b);

                var company = await _context.Companies.FindAsync(b.CompanyId);
                dto.CompanyName = company.Name;

                var rating = await _context.Ratings.FindAsync(company.RatingId);
                dto.RatingTotal = rating.TotalRating;
                dto.RatingAnimals = rating.AnimalsRating;
                dto.RatingPlanet = rating.PlanetRating;
                dto.RatingPeople = rating.PeopleRating;

                brandsDto.Add(dto);
            }

            var sortOptions = new Dictionary<string, List<BrandOutMultiDto>>()
            {
                {"any", brandsDto},
                {"total", brandsDto.OrderByDescending(b => b.RatingTotal).ToList()},
                {"planet", brandsDto.OrderByDescending(b => b.RatingPlanet).ToList()},
                {"people", brandsDto.OrderByDescending(b => b.RatingPeople).ToList()},
                {"animals", brandsDto.OrderByDescending(b => b.RatingAnimals).ToList()}
            };

            return sortOptions[sortType];
        }

        public async Task<bool> KeyExists(int key)
        {
            if (await _context.Brands.FindAsync(key) == null)
                return false;

            return true;
        }

        public async Task<int> Count()
        {
            return await _context.Brands.CountAsync();
        }
    }
}
