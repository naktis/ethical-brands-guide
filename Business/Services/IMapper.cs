using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Data.Models;
using System.Collections.Generic;

namespace Business.Services
{
    public interface IMapper
    {
        public Category CategoryFromDto(CategoryInDto dto);
        public CategoryOutDto CategoryToDto(Category entity);
        public IEnumerable<CategoryOutDto> CategoryToDto(IEnumerable<Category> entity);

        public Brand BrandFromDto(BrandInDto dto);
        public BrandOutDto BrandToDto(Brand entity);
        public IEnumerable<BrandOutDto> BrandToDto(IEnumerable<Brand> entity);

        public Company CompanyFromDto(CompanyInDto dto);
        public CompanyOutDto CompanyToDto(Company entity);
        public IEnumerable<CompanyOutDto> CompanyToDto(IEnumerable<Company> entity);

        public Rating RatingFromDto(RatingInDto dto);
        public RatingOutDto RatingToDto(Rating entity);

        public User UserFromDto(UserInDto dto);
        public UserOutDto UserToDto(User entity);
        public IEnumerable<UserOutDto> UserToDto(IEnumerable<User> entity);
    }
}
