using Api.RequestProcessors.Validators.Interfaces;
using Business.Dto.InputDto;
using System;
using System.Linq;

namespace Api.RequestProcessors.Validators
{
    public class UserValidator : IUserValidator
    {
        public readonly int MinPassLength = 5;
        public readonly int MaxPassLength = 20;
        public readonly int MinNameLength = 3;
        public readonly int MaxNameLength = 20;
        public readonly ISharedValidator _sharedValidator;

        public UserValidator(ISharedValidator sharedValidator)
        {
            _sharedValidator = sharedValidator;
        }

        public bool Validate(UserInDto user)
        {
            return ValidateUsername(user.Username) && ValidatePassword(user.Password);
        }

        public bool ValidateLogin(LoginDto user)
        {
            return ValidateUsername(user.Username) && ValidatePassword(user.Password);
        }

        private bool ValidateUsername(string username)
        {
            return username.All(u => char.IsLetterOrDigit(u)) &&
                   _sharedValidator.TextGoodLength(username, MaxNameLength, MinNameLength);
        }

        private bool ValidatePassword(string password)
        {
            return _sharedValidator.TextGoodLength(password, MaxPassLength, MinPassLength);
        }
    }
}
