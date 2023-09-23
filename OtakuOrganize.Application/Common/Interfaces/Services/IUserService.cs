using OtakuOrganize.Application.Models.InputModels;
using OtakuOrganize.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Application.Common.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetUsers();
        Task<UserViewModel> GetByIdUser(Guid? id);
        Task PostUser(UserInputModel user);
        Task DeleteUser(Guid? id);
        void PutUser(Guid? id, UserInputModel user);

        Task<string> Login(LoginInputModel user);


    }
}
