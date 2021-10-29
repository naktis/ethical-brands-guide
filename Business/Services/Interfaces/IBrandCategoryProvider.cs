using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IBrandCategoryProvider
    {
        public Task AddRange(IEnumerable<int> categoryIds, Brand brand);
        public Task RemoveRangeByCategory(int key);
        public Task RemoveRangeByBrand(int key);
    }
}
