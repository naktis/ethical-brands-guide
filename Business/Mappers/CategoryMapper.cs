using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Mappers.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappers
{
    public class CategoryMapper : ICategoryMapper
    {
        public CategoryOutDto EntityToDto(Category entity)
        {
            return new CategoryOutDto
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name
            };
        }

        public Category EntityFromDto(CategoryInDto dto)
        {
            return new Category
            {
                Name = dto.Name
            };
        }

        public IEnumerable<CategoryOutDto> EntityToDto(IEnumerable<Category> entities)
        {
            var categories = new List<CategoryOutDto>();

            foreach (var e in entities)
                categories.Add(EntityToDto(e));

            return categories;
        }

        public Category CopyFromDto(Category oldCategory, CategoryInDto newCategory)
        {
            oldCategory.Name = newCategory.Name;

            return oldCategory;
        }
    }
}
