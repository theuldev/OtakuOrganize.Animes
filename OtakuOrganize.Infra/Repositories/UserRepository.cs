using OtakuOrganize.Core.Entities;
using OtakuOrganize.Core.Interfaces.Repositories;
using OtakuOrganize.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Infra.Repositories
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

        public async Task Post(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll()
        {
            var users = await context.Users.ToListAsync();

            if (users.Count == 0) throw new NullReferenceException();
            return users;
        }

        public void Put(User user)
        {
            context.Update(user);
            context.SaveChanges();
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);
            return user != null ? user : throw new NullReferenceException();
        }
    }
}
