using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Data.Models;
using System.Collections.Generic;

namespace Business.Services
{
    public class Mapper : IMapper
    {
        public BrandOutDto BrandToDto(Brand entity)
        {
            return new BrandOutDto
            {
                BrandId = entity.BrandId,
                Name = entity.Name,
                Description = entity.Description,
            };
        }

        public Brand BrandFromDto(BrandInDto dto)
        {
            return new Brand
            {
                Name = dto.Name,
                Description = dto.Description,
            };
        }

        public CategoryOutDto CategoryToDto(Category entity)
        {
            return new CategoryOutDto
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name
            };
        }

        public Category CategoryFromDto(CategoryInDto dto)
        {
            return new Category
            {
                Name = dto.Name
            };
        }

        public CompanyOutDto CompanyToDto(Company company, Rating rating)
        {
            return new CompanyOutDto
            {
                CompanyId = company.CompanyId,
                Name = company.Name,
                Description = company.Description,
                Rating = RatingToDto(rating)
            };
        }

        public Company CompanyFromDto(CompanyInDto dto)
        {
            return new Company
            {
                Name = dto.Name,
                Description = dto.Description,
                Rating = RatingFromDto(dto.Rating)
            };
        }

        public RatingOutDto RatingToDto(Rating entity)
        {
            return new RatingOutDto
            {
                PlanetRating = entity.PlanetRating,
                PeopleRating = entity.PeopleRating,
                AnimalsRating = entity.AnimalsRating,
                TotalRating = entity.TotalRating,
                Description = entity.Description
            };
        }

        public Rating RatingFromDto(RatingInDto dto)
        {
            return new Rating
            {
                PlanetRating = dto.PlanetRating,
                PeopleRating = dto.PeopleRating,
                AnimalsRating = dto.AnimalsRating,
                Description = dto.Description
            };
        }

        public IEnumerable<CategoryOutDto> CategoryToDto(IEnumerable<Category> entities)
        {
            var categories = new List<CategoryOutDto>();

            foreach (var e in entities)
                categories.Add(CategoryToDto(e));

            return categories;
        }

        public IEnumerable<LightBrandOutDto> BrandToDto(IEnumerable<Brand> entities)
        {
            var brands = new List<LightBrandOutDto>();

            foreach (var e in entities)
                brands.Add(BrandToLightDto(e));

            return brands;
        }

        public IEnumerable<LightCompanyOutDto> CompanyToDto(IEnumerable<Company> entities)
        {
            var companies = new List<LightCompanyOutDto>();

            foreach (var e in entities)
                companies.Add(CompanyToLightDto(e));

            return companies;
        }

        public Category CopyFromDto(Category oldCategory, CategoryInDto newCategory)
        {
            oldCategory.Name = newCategory.Name;

            return oldCategory;
        }

        public Company CopyFromDto(Company oldCompany, CompanyInDto newCompany)
        {
            oldCompany.Name = newCompany.Name;
            oldCompany.Description = newCompany.Description;
            oldCompany.Rating = RatingFromDto(newCompany.Rating);

            return oldCompany;
        }

        public Brand CopyFromDto(Brand oldBrand, BrandInDto newBrand)
        {
            oldBrand.Name = newBrand.Name;
            oldBrand.Description = newBrand.Description;
            oldBrand.CompanyId = newBrand.CompanyId;

            return oldBrand;
        }

        private LightCompanyOutDto CompanyToLightDto(Company company)
        {
            return new LightCompanyOutDto
            {
                CompanyId = company.CompanyId,
                Name = company.Name
            };
        }

        public LightBrandOutDto BrandToLightDto(Brand entity)
        {
            return new LightBrandOutDto
            {
                BrandId = entity.BrandId,
                BrandName = entity.Name
            };
        }
    }
}
