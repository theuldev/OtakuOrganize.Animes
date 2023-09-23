using OtakuOrganize.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Application.Common.Interfaces.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, RoleType role);
    }
}
