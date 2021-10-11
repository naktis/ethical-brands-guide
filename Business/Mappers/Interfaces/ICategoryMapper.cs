using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Data.Models;
using System.Collections.Generic;

namespace Business.Mappers.Interfaces
{
    public interface ICategoryMapper : IMapper<Category, CategoryInDto>
    {
        public CategoryOutDto EntityToDto(Category entity);
        public IEnumerable<CategoryOutDto> EntityToDto(IEnumerable<Category> entity);
        public Category CopyFromDto(Category oldCategory, CategoryInDto newCategory);
    }
}
