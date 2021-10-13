using Data.Models;

namespace Business.Mappers.Interfaces
{
    public interface IBrandCategoryMapper
    {
        public BrandCategory NewEntity(Category category, Brand brand);
    }
}
