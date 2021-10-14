using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface ICategoryProvider : IProvider<CategoryInDto, CategoryOutDto>
    {
        public Task<CategoryOutDto> Add(CategoryInDto category);
        public Task<IEnumerable<CategoryOutDto>> GetAll();
        public Task<bool> Exists(CategoryInDto category);
        public Task<bool> AllExist(List<int> categoryIds);
    }
}
