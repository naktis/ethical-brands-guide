using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Calculators;
using Data.Models;
using System.Linq;
using System;

namespace Business.Services
{
    public class BrandProvider : IBrandProvider
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BrandProvider(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LightBrandOutDto> Add(BrandInDto dto)
        {
            var entity = _mapper.BrandFromDto(dto);
            entity.Company = await _context.Companies.FindAsync(dto.CompanyId);

            if (entity.Company.Brands == null)
                entity.Company.Brands = new List<Brand>();

            entity.Company.Brands.Add(entity);
            var createdEntity = await _context.Brands.AddAsync(entity);
            await _context.SaveChangesAsync();

            // Map brand with categories
            if(dto.CategoryIds.Any())
            {
                foreach (var categoryId in dto.CategoryIds)
                {
                    var category = await _context.Categories.FindAsync(categoryId);

                    var brandCategory = new BrandCategory
                    {
                        Brand = createdEntity.Entity,
                        Category = category
                    };

                    var createdBrandCategory = await _context.BrandsCategories.AddAsync(brandCategory);
                    createdEntity.Entity.BrandsCategories.Add(createdBrandCategory.Entity);
                    category.BrandsCategories.Add(createdBrandCategory.Entity);
                }
                await _context.SaveChangesAsync();
            }

            return _mapper.BrandToLightDto(createdEntity.Entity);
        }
        
        public async Task Delete(int key)
        {
            var brand = await _context.Brands.FindAsync(key);
            var brandCategories = _context.BrandsCategories.Where(x => x.BrandId == key).ToList();

            _context.BrandsCategories.RemoveRange(brandCategories);
            _context.Brands.Remove(brand);

            await _context.SaveChangesAsync();
        }
        
        public async Task Update(int key, BrandInDto newBrand)
        {
            var oldBrand = await _context.Brands.FindAsync(key);
            oldBrand = _mapper.CopyFromDto(oldBrand, newBrand);
            oldBrand.Company = await _context.Companies.FindAsync(oldBrand.CompanyId);

            // Remove old brandscategories
            var brandCategories = _context.BrandsCategories.Where(x => x.BrandId == key).ToList();
            foreach (var bc in brandCategories)
                _context.BrandsCategories.Remove(bc);

            // Add new brandscategories
            foreach (var categoryId in newBrand.CategoryIds)
            {
                var category = await _context.Categories.FindAsync(categoryId);

                var brandCategory = new BrandCategory
                {
                    Brand = oldBrand,
                    Category = category
                };

                var createdBrandCategory = await _context.BrandsCategories.AddAsync(brandCategory);
                oldBrand.BrandsCategories.Add(createdBrandCategory.Entity);
                category.BrandsCategories.Add(createdBrandCategory.Entity);
            }

            await _context.SaveChangesAsync();
        }
        
        public async Task<BrandOutDto> Get(int key)
        {
            var brand = await _context.Brands.FindAsync(key);
            var brandCategories = _context.BrandsCategories.Where(x => x.BrandId == key).ToList();
            var company = await _context.Companies.FindAsync(brand.CompanyId);
            var rating = await _context.Ratings.FindAsync(company.RatingId);

            var brandDto = _mapper.BrandToDto(brand);
            brandDto.Company = _mapper.CompanyToDto(company, rating);

            brandDto.Categories = new List<CategoryOutDto>();
            foreach (var bc in brandCategories)
            {
                var category = await _context.Categories.FindAsync(bc.CategoryId);
                brandDto.Categories.Add(_mapper.CategoryToDto(category));
            }

            return brandDto;
        }

        public async Task<IEnumerable<LightBrandOutDto>> Get(string query, string sortType = "any", int categoryId = 0)
        {
            var brands = new List<Brand>();

            if (query == null && categoryId == 0)
                brands = _context.Brands.ToList();
            else if (query != null && categoryId == 0)
                brands = _context.Brands.Where(b => b.Name.Contains(query)).ToList();
            else if (query == null && categoryId != 0)
                brands = _context.Brands.Where(
                    b => b.BrandsCategories.Any(c => c.Category.CategoryId == categoryId))
                    .ToList();
            else // query != null && categoryId != null
                brands = _context.Brands.Where(
                    b => b.BrandsCategories.Any(c => c.Category.CategoryId == categoryId) && 
                    b.Name.Contains(query))
                    .ToList();

            var brandsDto = new List<LightBrandOutDto>();

            foreach (var b in brands)
            {
                var dto = _mapper.BrandToLightDto(b);

                var company = await _context.Companies.FindAsync(b.CompanyId);
                dto.CompanyName = company.Name;

                var rating = await _context.Ratings.FindAsync(company.RatingId);
                dto.RatingTotal = rating.TotalRating;
                dto.RatingAnimals = rating.AnimalsRating;
                dto.RatingPlanet = rating.PlanetRating;
                dto.RatingPeople = rating.PeopleRating;

                brandsDto.Add(dto);
            }

            var sortBrands = new Dictionary<string, List<LightBrandOutDto>>()
            {
                {"any", brandsDto},
                {"total", brandsDto.OrderByDescending(b => b.RatingTotal).ToList()},
                {"planet", brandsDto.OrderByDescending(b => b.RatingPlanet).ToList()},
                {"people", brandsDto.OrderByDescending(b => b.RatingPeople).ToList()},
                {"animals", brandsDto.OrderByDescending(b => b.RatingAnimals).ToList()}
            };

            return sortBrands[sortType];
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
