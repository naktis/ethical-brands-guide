using Business.Mappers.Interfaces;
using Data.Models;

namespace Business.Mappers
{
    public class BrandCategoryMapper : IBrandCategoryMapper
    {
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
