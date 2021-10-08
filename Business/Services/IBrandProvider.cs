using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IBrandProvider
    {
        public Task<LightBrandOutDto> Add(BrandInDto dto);
        public Task Delete(int key);
        public Task Update(int key, BrandInDto dto);
        public IEnumerable<LightBrandOutDto> Get(string query);
        public Task<BrandOutDto> Get(int key);
        public Task<bool> KeyExists(int key);
    }
}
