using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Application.Common.Interfaces.Services
{
    public interface ISecurityService
    {
        Task<string> EncryptPassword(string password);
        Task<string> DecryptPassword(string password);
        Task<bool> VerifyPassword(string passtoVerify,string passwordEncrypt);
    }
}
