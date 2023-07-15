using AnimesControl.Core.Entities;
using AnimesControl.Core.Interfaces.Repositories;
using AnimesControl.Infra.Context;
using Microsoft.EntityFrameworkCore;
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
        public async Task<User> GetById(Guid? id)
        {
            var user = await context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null) throw new NullReferenceException();
            return user;

        }

        public void Delete(User user)
        {
            var userModel = GetById(user.Id);

            if (userModel == null) throw new NullReferenceException();
            context.Entry(userModel).State = EntityState.Deleted;
        }

        public void Post(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }


        public async Task<List<User>> GetAll()
        {
            var users = await context.Users.ToListAsync();

            if (users.Count == 0) throw new NullReferenceException();
            return users;
        }

        public void Put(User user)
        {
            var userFilterbyId = GetById(user.Id);
            context.Entry(userFilterbyId).CurrentValues.SetValues(user);
            context.SaveChanges();

        }
    }
}
