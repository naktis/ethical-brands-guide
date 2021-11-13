using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Mappers.Interfaces;
using Business.Security;
using Business.Services.Interfaces;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserProvider : IUserProvider
    {
        private readonly AppDbContext _context;
        private readonly IUserMapper _mapper;
        private readonly IHasher _hasher;
        private readonly IGenerator _generator;

        public UserProvider(AppDbContext context, IUserMapper mapper, IHasher hasher,
            IGenerator generator)
        {
            _context = context;
            _mapper = mapper;
            _hasher = hasher;
            _generator = generator;
        }

        public async Task<UserOutDto> Add(UserInDto userDto)
        {
            var user = _mapper.EntityFromDto(userDto);
            var password = _generator.GeneratePassword();
            user.Password = _hasher.Hash(password);

            var createdUser = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // TODO: send email with password

            return _mapper.EntityToDto(createdUser.Entity);
        }

        public async Task<LoginResultDto> Authenticate(LoginDto request)
        {
            var user = await GetUserByEmailPassword(request);

            if (user != null)
            {
                var userDto = _mapper.EntityToLoginDto(user);
                userDto.Token = _generator.GenerateJwt(userDto);
                return userDto;
            }

            return null;
        }

        public async Task Delete(int key)
        {
            var user = await _context.Users.FindAsync(key);
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(UserInDto request)
        {
            var username = await GetUserByUsername(request.Username);
            var email = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            return (username != null || email != null);
        }

        public async Task<UserOutDto> Get(int key)
        {
            var user = await _context.Users.FindAsync(key);

            if (user != null)
                return _mapper.EntityToDto(user);
            return null;
        }

        public async Task<bool> KeyExists(int key)
        {
            return await Get(key) != null;
        }

        private async Task<User> GetUserByEmailPassword(LoginDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user != null)
            {
                if (_hasher.Check(user.Password, request.Password))
                    return user;
            }

            return null;
        }

        private async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> EmailMatchesPass(LoginDto request)
        {
            return await GetUserByEmailPassword(request) != null;
        }
    }
}
