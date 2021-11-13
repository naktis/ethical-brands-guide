using Business.Dto.OutputDto;

namespace Business.Security
{
    public interface IGenerator
    {
        public string GenerateJwt(UserOutDto user);

        public string GeneratePassword();
    }
}
