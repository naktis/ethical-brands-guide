using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Data.Models;

namespace Business.Mappers.Interfaces
{
    public interface IBrandMapper : IMapper<Brand, BrandInDto>
    {
        public BrandOutDto EntityToDto(Brand entity);
        public Brand CopyFromDto(Brand oldBrand, BrandInDto newBrand);
        public BrandOutMultiDto EntityToMultiDto(Brand entity);
        public BrandOutPostDto EntityToPostDto(Brand entity);
    }
}
