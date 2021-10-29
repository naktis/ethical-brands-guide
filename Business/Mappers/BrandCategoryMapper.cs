using Business.Dto.OutputDto;
using Business.Mappers.Interfaces;
using Data.Models;
using System.Collections.Generic;

namespace Business.Mappers
{
    public class BrandCategoryMapper : IBrandCategoryMapper
    {
        private readonly ICategoryMapper _categoryMapper;

        public BrandCategoryMapper(ICategoryMapper categoryMapper)
        {
            _categoryMapper = categoryMapper;
        }

        public IEnumerable<CategoryOutDto> EntitiesToCategoryDtos(IEnumerable<BrandCategory> bcs)
        {
            var categories = new List<Category>();

            foreach (var bc in bcs)
                categories.Add(bc.Category);

            return _categoryMapper.EntitiesToDtos(categories);
        }

        public BrandCategory NewEntity(Category category, Brand brand)
        {
            return new BrandCategory
            {
                Brand = brand,
                Category = category
            };
        }
    }
}
