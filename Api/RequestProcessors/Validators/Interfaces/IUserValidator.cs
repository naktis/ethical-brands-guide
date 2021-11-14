using Business.Dto.InputDto;

namespace Api.RequestProcessors.Validators.Interfaces
{
    public interface IUserValidator : IValidator<UserInDto>
    {
        public bool ValidateLogin(LoginDto user);
    }
}
