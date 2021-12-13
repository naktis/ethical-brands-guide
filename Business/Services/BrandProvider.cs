using Business.Dto.InputDto;
using Business.Dto.InputDto.RequestParameters;
using Business.Dto.OutputDto;
using Business.Mappers.Interfaces;
using Business.Services.Interfaces;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
        private readonly Dictionary<string, Func<Brand, double>> sortFunctions;

        public BrandProvider(AppDbContext context, IBrandMapper mapper, 
            IBrandCategoryProvider bcProvider)
        {
            _context = context;
            _mapper = mapper;
            _bcProvider = bcProvider;

            sortFunctions = new Dictionary<string, Func<Brand, double>>()
            {
                {"any", b => b.BrandId },
                {"total", b => b.Company.Rating.TotalRating },
                {"planet", b => b.Company.Rating.PlanetRating },
                {"people", b => b.Company.Rating.PeopleRating },
                {"animals", b => b.Company.Rating.AnimalsRating }
            };
        }

        public async Task<BrandOutPostDto> Add(BrandInDto dto, int creatorId)
        {
            var entity = _mapper.EntityFromDto(dto);

            entity.CreatorId = creatorId;

            var company = await _context.Companies.FindAsync(dto.CompanyId);
            entity.Company = company;

            Request request = new Request();
            if (dto.RequestId != null) 
            {
                request = await _context.Requests.FindAsync(dto.RequestId);
                entity.Request = request;
            }

            var createdBrand = await _context.Brands.AddAsync(entity);
            company.Brands.Add(createdBrand.Entity);

            if (dto.RequestId != null)
            {
                request.Brand = createdBrand.Entity;
            }

            await _context.SaveChangesAsync();

            if (dto.CategoryIds.Any())
                await _bcProvider.AddRange(dto.CategoryIds, createdBrand.Entity);
            
            return _mapper.EntityToPostDto(createdBrand.Entity);
        }
        
        public async Task Delete(int key)
        {
            var brand = await _context.Brands.FindAsync(key);
            var brandCategories = await _context.BrandsCategories.Where(x => x.BrandId == key).ToListAsync();
            var request = await _context.Requests.FirstOrDefaultAsync(x => x.BrandId == key);

            if (request != null)
                request.Brand = null;

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
                .Include(b => b.Creator)
                .Include(b => b.Company)
                .ThenInclude(c => c.Rating)
                .Include(b => b.BrandsCategories)
                .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync();

            var brandDto = _mapper.EntityToDto(brand);

            if (brand.Creator != null)
                brandDto.CreatorName = brand.Creator.Username;

            return brandDto;
        }

        public IEnumerable<BrandOutMultiDto> Get(BrandParameters brandParams)
        {
            var brands = new List<Brand>();

            if (brandParams.Query == null && brandParams.CategoryId == 0)
                brands = _context.Brands
                    .Include(b => b.Company)
                    .ThenInclude(c => c.Rating)
                    .OrderByDescending(sortFunctions[brandParams.SortType])
                    .Skip((brandParams.PageNumber - 1) * brandParams.PageSize)
                    .Take(brandParams.PageSize)
                    .ToList();
            else if (brandParams.Query != null && brandParams.CategoryId == 0)
                brands = _context.Brands
                    .Where(b => b.Name.Contains(brandParams.Query))
                    .Include(b => b.Company)
                    .ThenInclude(c => c.Rating)
                    .OrderByDescending(sortFunctions[brandParams.SortType])
                    .Skip((brandParams.PageNumber - 1) * brandParams.PageSize)
                    .Take(brandParams.PageSize)
                    .ToList();
            else if (brandParams.Query == null && brandParams.CategoryId != 0)
                brands = _context.Brands
                    .Where(b => b.BrandsCategories
                        .Any(c => c.Category.CategoryId == brandParams.CategoryId))
                    .Include(b => b.Company)
                    .ThenInclude(c => c.Rating)
                    .OrderByDescending(sortFunctions[brandParams.SortType])
                    .Skip((brandParams.PageNumber - 1) * brandParams.PageSize)
                    .Take(brandParams.PageSize)
                    .ToList();
            else // query != null && categoryId != null
                brands = _context.Brands.Where(
                    b => b.BrandsCategories
                    .Any(c => c.Category.CategoryId == brandParams.CategoryId) &&
                              b.Name.Contains(brandParams.Query))
                    .Include(b => b.Company)
                    .ThenInclude(c => c.Rating)
                    .OrderByDescending(sortFunctions[brandParams.SortType])
                    .Skip((brandParams.PageNumber - 1) * brandParams.PageSize)
                    .Take(brandParams.PageSize)
                    .ToList();

            return _mapper.EntitiesToDtos(brands);
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

        public async Task<bool> Exists(BrandInDto brand)
        {
            return !(await _context.Brands
                .FirstOrDefaultAsync(b => b.Name == brand.Name && b.CompanyId == brand.CompanyId) == null);
        }

        public async Task<bool> NameAvailable(BrandInDto brand, int key)
        {
            return await _context.Brands
                .FirstOrDefaultAsync(b => 
                    b.Name == brand.Name && 
                    b.CompanyId == brand.CompanyId &&
                    b.BrandId != key
                ) == null;
        }
    }
}
