using AnimesControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        void Post(User user);
        void Delete(User user);
        Task<User> GetById(Guid? id);

        Task<List<User>> GetAll();

        void Put(User user);
    }
}
