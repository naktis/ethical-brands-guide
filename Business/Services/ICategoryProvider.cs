using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ICategoryProvider
    {
        public Task<CategoryOutDto> Add(CategoryInDto category);
        public Task Delete(int key);
        public Task Update(int key, CategoryInDto newCategory);
        public Task<CategoryOutDto> Get(int key);
        public Task<IEnumerable<CategoryOutDto>> GetAll();
        public Task<bool> KeyExists(int key);
        public Task<bool> Exists(CategoryInDto category);
    }
}
