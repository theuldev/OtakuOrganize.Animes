using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Core.Entities;
using AnimesControl.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Infra.Repositories
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

        public async Task<Customer> GetByIdCustomer(int? id)
        {
            var customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null) throw new NullReferenceException();
            return customer;
        }

        public void PostCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void PutCustomer(Customer customer)
        {
            var customerData = context.Customers.Where(c => c.Id == customer.Id).FirstOrDefault() ;
            context.Entry(customerData).CurrentValues.SetValues(customer);
            context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {

   
            context.Entry(customer).State = EntityState.Deleted;
            context.SaveChanges();

        }

        public void AddAnimeCustomer(Anime_Customer anime_Customer)
        {
            context.Anime_Customer.Add(anime_Customer);
            context.SaveChanges();
        }

    }
}
