using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Mappers.Interfaces;
using Data.Models;

namespace Business.Mappers
{
    public class BrandMapper : IBrandMapper 
    {
        public BrandOutDto EntityToDto(Brand entity)
        {
            return new BrandOutDto
            {
                BrandId = entity.BrandId,
                Name = entity.Name,
                Description = entity.Description,
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
                BrandName = entity.Name
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
    }
}
