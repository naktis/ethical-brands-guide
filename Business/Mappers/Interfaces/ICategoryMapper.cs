using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Data.Models;
using System.Collections.Generic;

namespace Business.Mappers.Interfaces
{
    public interface ICategoryMapper : IMapper<Category, CategoryInDto, CategoryOutDto>
    {
        public IEnumerable<CategoryOutDto> EntitiesToDtos(IEnumerable<Category> entity);
    }
}
