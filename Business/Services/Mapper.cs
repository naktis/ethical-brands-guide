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
                CreatorId = entity.CreatorId,
                CompanyId = entity.CompanyId
            };
        }

        public Brand BrandFromDto(BrandInDto dto)
        {
            return new Brand
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatorId = dto.CreatorId,
                CompanyId = dto.CompanyId
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

        public CompanyOutDto CompanyToDto(Company entity)
        {
            return new CompanyOutDto
            {
                CompanyId = entity.CompanyId,
                Name = entity.Name,
                Description = entity.Description,
                RatingId = entity.RatingId
            };
        }

        public Company CompanyFromDto(CompanyInDto dto)
        {
            return new Company
            {
                Name = dto.Name,
                Description = dto.Description,
                RatingId = dto.RatingId
            };
        }

        public RatingOutDto RatingToDto(Rating entity)
        {
            return new RatingOutDto
            {
                RatingId = entity.RatingId,
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

        public UserOutDto UserToDto(User entity)
        {
            return new UserOutDto
            {
                Name = entity.Name,
                Type = entity.Type
            };
        }

        public User UserFromDto(UserInDto dto)
        {
            return new User
            {
                Name = dto.Name,
                Password = dto.Password,
                Type = dto.Type
            };
        }

        public IEnumerable<CategoryOutDto> CategoryToDto(IEnumerable<Category> entities)
        {
            var categories = new List<CategoryOutDto>();

            foreach (var e in entities)
                categories.Add(CategoryToDto(e));

            return categories;
        }

        public IEnumerable<BrandOutDto> BrandToDto(IEnumerable<Brand> entities)
        {
            var brands = new List<BrandOutDto>();

            foreach (var e in entities)
                brands.Add(BrandToDto(e));

            return brands;
        }

        public IEnumerable<CompanyOutDto> CompanyToDto(IEnumerable<Company> entities)
        {
            var companies = new List<CompanyOutDto>();

            foreach (var e in entities)
                companies.Add(CompanyToDto(e));

            return companies;
        }

        public IEnumerable<UserOutDto> UserToDto(IEnumerable<User> entities)
        {
            var users = new List<UserOutDto>();

            foreach (var e in entities)
                users.Add(UserToDto(e));

            return users;
        }
    }
}
