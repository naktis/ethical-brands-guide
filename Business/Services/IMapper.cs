﻿using Business.Dto.InputDto;
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
        public IEnumerable<BrandOutDto> BrandToDto(IEnumerable<Brand> entity);

        public Company CompanyFromDto(CompanyInDto dto);
        public CompanyOutDto CompanyToDto(Company company, Rating rating);
        public MultiCompanyOutDto MultiCompanyToDto(Company company);
        public IEnumerable<MultiCompanyOutDto> CompanyToDto(IEnumerable<Company> entity);
        public Company CopyFromDto(Company oldCompany, CompanyInDto newCompany);

        public Rating RatingFromDto(RatingInDto dto);
        public RatingOutDto RatingToDto(Rating entity);

        public User UserFromDto(UserInDto dto);
        public UserOutDto UserToDto(User entity);
        public IEnumerable<UserOutDto> UserToDto(IEnumerable<User> entity);
    }
}
