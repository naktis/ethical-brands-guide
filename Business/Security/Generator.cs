using Business.Dto.OutputDto;
using Business.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Security
{
    public class Generator : IGenerator
    {
        private readonly AccountOptions _options;

        public Generator(IOptions<AccountOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateJwt(UserOutDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Secret);

            var claims = new List<Claim>
            {
                new Claim("id", user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Type.ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(_options.Expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GeneratePassword()
        {
            var rnd = new Random();
            int length = rnd.Next(8, 16);
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var password = new StringBuilder();

            while (0 < length--)
            {
                password.Append(valid[rnd.Next(valid.Length)]);
            }

            return password.ToString();
        }
    }
}
