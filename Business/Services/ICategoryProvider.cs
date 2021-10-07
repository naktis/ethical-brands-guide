using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ICategoryProvider
    {
        public Task<CategoryOutDto> Add(CategoryInDto category);
        public Task Delete(int id);
        public Task Edit(CategoryInDto category);
        public Task<CategoryOutDto> Get(int id);
        public Task<IEnumerable<CategoryOutDto>> GetAll();
        public Task<bool> ExistsByKey(int id);
        public Task<bool> ExistsByName(string name);
    }
}
