using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IBrandProvider : IProvider<BrandInDto, BrandOutDto>
    {
        public Task<BrandOutPostDto> Add(BrandInDto dto);
        public Task<IEnumerable<BrandOutMultiDto>> Get(string query, string sortType, int categoryId);
        public Task<int> Count();
    }
}
