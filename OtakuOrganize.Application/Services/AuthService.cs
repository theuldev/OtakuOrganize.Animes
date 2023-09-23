using OtakuOrganize.Application.Common.Interfaces.Services;
using OtakuOrganize.Core.Enums;
using FluentValidation.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Application.Services
{
    public class AuthService : IAuthService
    {
        public IConfiguration configuration;
        public AuthService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string GenerateJwtToken(string email, RoleType role)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = configuration["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("Username",email),
                new Claim(ClaimTypes.Role,role.ToString())
            };
            var token = new JwtSecurityToken(issuer: issuer, audience: audience, expires: DateTime.Now.AddHours(1), signingCredentials: credentials, claims: claims);

            var tokenHandler = new JwtSecurityTokenHandler();
             
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
