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
        public Category CopyFromDto(Category oldCategory, CategoryInDto newCategory);

        public Brand BrandFromDto(BrandInDto dto);
        public BrandOutDto BrandToDto(Brand entity);
        public IEnumerable<LightBrandOutDto> BrandToDto(IEnumerable<Brand> entity);
        public Brand CopyFromDto(Brand oldBrand, BrandInDto newBrand);
        public LightBrandOutDto BrandToLightDto(Brand entity);

        public Company CompanyFromDto(CompanyInDto dto);
        public CompanyOutDto CompanyToDto(Company company, Rating rating);
        public IEnumerable<LightCompanyOutDto> CompanyToDto(IEnumerable<Company> entity);
        public Company CopyFromDto(Company oldCompany, CompanyInDto newCompany);

        public Rating RatingFromDto(RatingInDto dto);
        public RatingOutDto RatingToDto(Rating entity);
    }
}
