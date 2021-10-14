using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Mappers.Interfaces;
using Data.Models;
using System.Collections.Generic;

namespace Business.Mappers
{
    public class BrandMapper : IBrandMapper 
    {
        private readonly ICompanyMapper _companyMapper;
        private readonly IBrandCategoryMapper _bcMapper;

        public BrandMapper(ICompanyMapper companyMapper, IBrandCategoryMapper bcMapper)
        {
            _companyMapper = companyMapper;
            _bcMapper = bcMapper;
        }

        public BrandOutDto EntityToDto(Brand entity)
        {
            return new BrandOutDto
            {
                BrandId = entity.BrandId,
                Name = entity.Name,
                Description = entity.Description,
                Company = _companyMapper.EntityToDto(entity.Company),
                Categories = _bcMapper.EntitiesToCategoryDtos(entity.BrandsCategories)
            };
        }

        public Brand EntityFromDto(BrandInDto dto)
        {
            return new Brand
            {
                Name = dto.Name,
                Description = dto.Description,
            };
        }

        public Brand CopyFromDto(Brand oldBrand, BrandInDto newBrand)
        {
            oldBrand.Name = newBrand.Name;
            oldBrand.Description = newBrand.Description;
            oldBrand.CompanyId = newBrand.CompanyId;

            return oldBrand;
        }

        public BrandOutMultiDto EntityToMultiDto(Brand entity)
        {
            return new BrandOutMultiDto
            {
                BrandId = entity.BrandId,
                BrandName = entity.Name,
                CompanyName = entity.Company.Name,
                RatingTotal = entity.Company.Rating.TotalRating,
                RatingAnimals = entity.Company.Rating.AnimalsRating,
                RatingPeople = entity.Company.Rating.PeopleRating,
                RatingPlanet = entity.Company.Rating.PlanetRating
            };
        }

        public BrandOutPostDto EntityToPostDto(Brand entity)
        {
            return new BrandOutPostDto
            {
                BrandId = entity.BrandId,
                Name = entity.Name
            };
        }

        public IList<BrandOutMultiDto> EntitiesToDtos(IEnumerable<Brand> entities)
        {
            var dtos = new List<BrandOutMultiDto>();

            foreach (var e in entities)
                dtos.Add(EntityToMultiDto(e));

            return dtos;
        }
    }
}
