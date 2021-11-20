using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Data.Models;

namespace Business.Mappers.Interfaces
{
    public interface IUserMapper : IMapper<User, UserInDto, UserOutDto>
    {
        public LoginResultDto EntityToLoginDto(User user);
    }
}
