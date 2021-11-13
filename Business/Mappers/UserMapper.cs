using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Mappers.Interfaces;
using Data.Models;
using System;

namespace Business.Mappers
{
    public class UserMapper : IUserMapper
    {
        public User CopyFromDto(User entity, UserInDto dto)
        {
            throw new NotImplementedException();
        }

        public User EntityFromDto(UserInDto user)
        {
            return new User
            {
                Username = user.Username,
                Email = user.Email,
                Type = UserType.Registered
            };
        }

        public UserOutDto EntityToDto(User user)
        {
            return new UserOutDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Type = user.Type
            };
        }

        public LoginResultDto EntityToLoginDto(User user)
        {
            return new LoginResultDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Type = user.Type
            };
        }
    }
}
