using AnimesControl.Application.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Application.Services
{
    public class SecurityService : ISecurityService
    {
        public Task<string> EncryptPassword(string password)
        {
            var passEncrypt = BCrypt.Net.BCrypt.HashPassword(password);
            return Task.FromResult(passEncrypt);
        }
        public Task<string> DecryptPassword(string passEncrypt)
        {
            var password = BCrypt.Net.BCrypt.EnhancedHashPassword(passEncrypt);
            return Task.FromResult(password);
        }
    }
}
