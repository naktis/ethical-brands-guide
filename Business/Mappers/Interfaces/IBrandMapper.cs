using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Data.Models;
using System.Collections.Generic;

namespace Business.Mappers.Interfaces
{
    public interface IBrandMapper : IMapper<Brand, BrandInDto, BrandOutDto>
    {
        public BrandOutMultiDto EntityToMultiDto(Brand entity);
        public BrandOutPostDto EntityToPostDto(Brand entity);
        public IList<BrandOutMultiDto> EntitiesToDtos(IEnumerable<Brand> entities);
    }
}
