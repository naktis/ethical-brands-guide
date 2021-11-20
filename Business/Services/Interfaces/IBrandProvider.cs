using Business.Dto.InputDto;
using Business.Dto.InputDto.RequestParameters;
using Business.Dto.OutputDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IBrandProvider : IProvider<BrandInDto, BrandOutDto>
    {
        public Task<BrandOutPostDto> Add(BrandInDto dto, int creatorId);
        public IEnumerable<BrandOutMultiDto> Get(BrandParameters parameters);
        public Task<int> Count();
        public Task RemoveRangeByCompany(int companyId);
    }
}
