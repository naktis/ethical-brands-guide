using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Mappers.Interfaces;
using Business.Security;
using Business.Services.Interfaces;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserProvider : IUserProvider
    {
        private readonly AppDbContext _context;
        private readonly IUserMapper _mapper;
        private readonly IHasher _hasher;
        private readonly IGenerator _generator;
        private readonly IDefaultSetter _defaultSetter;

        public UserProvider(AppDbContext context, IUserMapper mapper, IHasher hasher,
            IGenerator generator, IDefaultSetter defaultSetter)
        {
            _context = context;
            _mapper = mapper;
            _hasher = hasher;
            _generator = generator;
            _defaultSetter = defaultSetter;
        }

        public async Task<UserOutDto> Add(UserInDto userDto)
        {
            var user = _mapper.EntityFromDto(userDto);

            // user = _defaultSetter.SetDefaultUserType(user);
            // var password = _generator.GeneratePassword();

            user.Password = _hasher.Hash(userDto.Password);

            var createdUser = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(createdUser.Entity);
        }

        public async Task<LoginResultDto> Authenticate(LoginDto request)
        {
            var user = await GetUserByCredentials(request);

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
            return await GetUserByUsername(request.Username) != null;
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

        public async Task<IEnumerable<UserOutDto>> GetAll()
        {
            var users = new List<UserOutDto>();

            foreach (var u in await _context.Users.ToListAsync())
                users.Add(_mapper.EntityToDto(u));

            return users;
        }

        public async Task<bool> UsernameMatchesPass(LoginDto request)
        {
            return await GetUserByCredentials(request) != null;
        }

        private async Task<User> GetUserByCredentials(LoginDto request)
        {
            var user = await GetUserByUsername(request.Username);

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
    }
}
