using OtakuOrganize.Application.Common.Interfaces.Services;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Application.Services
{
    public class SecurityService : ISecurityService
    {
        public Task<string> EncryptPassword(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            var passEncrypt = BCrypt.Net.BCrypt.HashPassword(password,salt);
            return Task.FromResult(passEncrypt);
        }
        public Task<string> DecryptPassword(string passEncrypt)
        {
            var password = BCrypt.Net.BCrypt.HashPassword(passEncrypt);
            return Task.FromResult(password);
        }
        public Task<bool> VerifyPassword(string password, string passToVerify) {
   
            var result = BCrypt.Net.BCrypt.Verify(password, passToVerify);
            return Task.FromResult(result);
            
        }
    }
}
