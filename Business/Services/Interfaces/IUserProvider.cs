using Business.Dto.InputDto;
using Business.Dto.InputDto.RequestParameters;
using Business.Dto.OutputDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IUserProvider
    {
        public Task<LoginResultDto> Authenticate(LoginDto request);
        public Task<UserOutDto> Add(UserInDto dto);
        public Task Delete(int key);
        public Task<bool> KeyExists(int key);
        public Task<bool> Exists(UserInDto dto);
        public Task<bool> UsernameMatchesPass(LoginDto request);
        public IEnumerable<UserOutDto> GetAll(PagingParameters paging);
    }
}
