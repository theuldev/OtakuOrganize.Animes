using AnimesControl.Core.Entities;
using AnimesControl.Core.Interfaces.Repositories;
using AnimesControl.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AnimeContext context;
        public UserRepository(AnimeContext _context)
        {
            this.context = _context;
            
        }

        public void Delete(User user)
        {
          var userModel = context.Users.Where(u => u.Id == user.Id).FirstOrDefault();

            if (userModel == null) throw new NullReferenceException();
            context.Entry(userModel);
        }

        public void Post(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
