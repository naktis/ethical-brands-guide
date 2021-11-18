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
            entity.Username = dto.Username;
            entity.Type = (UserType)Enum.Parse(typeof(UserType), dto.Type);
            return entity;
        }

        public User EntityFromDto(UserInDto user)
        {
            return new User
            {
                Username = user.Username,
                Type = (UserType)Enum.Parse(typeof(UserType), user.Type)
        };
        }

        public UserOutDto EntityToDto(User user)
        {
            return new UserOutDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Type = user.Type.ToString()
            };
        }

        public LoginResultDto EntityToLoginDto(User user)
        {
            return new LoginResultDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Type = user.Type.ToString()
            };
        }
    }
}
