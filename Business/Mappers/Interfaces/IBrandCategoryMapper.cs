using Business.Dto.OutputDto;
using Data.Models;
using System.Collections.Generic;

namespace Business.Mappers.Interfaces
{
    public interface IBrandCategoryMapper
    {
        public IEnumerable<CategoryOutDto> EntitiesToCategoryDtos(IEnumerable<BrandCategory> bcs);
        public BrandCategory NewEntity(Category category, Brand brand);
    }
}
