using OtakuOrganize.Core.Entities;
using OtakuOrganize.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using OtakuOrganize.Core.Interfaces.Repositories;

namespace OtakuOrganize.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AnimeContext context;
        public CustomerRepository(AnimeContext _context)
        {
            this.context = _context;

        }
        public async Task<List<Customer>> GetCustomers()
        {
            return await context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdCustomer(Guid id)
        {
            var customer = await context.Customers.Where(c => c.Id == id).Include(c => c.User).SingleOrDefaultAsync();
            return customer != null ? customer : throw new NullReferenceException();
        }
        
        public async Task PostCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
        }

        public async Task PutCustomer(Customer customer)
        {
            context.Update(customer);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCustomer(Guid id)
        {
            var customer = GetByIdCustomer(id);
            context.Remove(customer.Result);
            await context.SaveChangesAsync();
        }

        public async Task AddAnimeCustomer(Anime_Customer anime_Customer)
        {
            context.Anime_Customer.Add(anime_Customer);
            await context.SaveChangesAsync();

        }

    }
}
