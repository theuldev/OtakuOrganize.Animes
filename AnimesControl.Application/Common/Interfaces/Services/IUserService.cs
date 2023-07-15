using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Application.Common.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetUsers();
        Task<UserViewModel> GetByIdUser(Guid? id);
        void PostUser(UserInputModel user);
        Task DeleteUser(Guid? id);
        void PutUser(Guid? id, UserInputModel user);


    }
}
