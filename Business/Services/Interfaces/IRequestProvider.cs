using Business.Dto.InputDto;
using Business.Dto.InputDto.RequestParameters;
using Business.Dto.OutputDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IRequestProvider
    {
        public Task<RequestOutDto> Add(RequestInDto request);
        public IEnumerable<RequestOutDto> GetAll(PagingParameters paging);
        public Task<bool> KeyExists(int key);
        public Task Delete(int key);
    }
}
