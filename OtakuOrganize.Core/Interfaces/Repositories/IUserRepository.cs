using OtakuOrganize.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task Post(User user);
        void Delete(User user);
        Task<User> GetById(Guid? id);


        Task<User> GetByEmail(string email);

        Task<List<User>> GetAll();

        void Put(User user);
    }
}
