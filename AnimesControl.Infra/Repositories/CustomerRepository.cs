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

        public async Task<Customer> GetByIdCustomer(Guid id)
        {
            var customer = await context.Customers.Where(c => c.Id == id).Include(c => c.User).FirstOrDefaultAsync();

            return customer;
        }

        public void PostCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void PutCustomer(Customer customer)
        {
            var customerFilterbyId = GetByIdCustomer(customer.Id);
            context.Entry(customerFilterbyId.Result).CurrentValues.SetValues(customer);
            context.SaveChanges();
        }

        public void DeleteCustomer(Customer customerDetails)
        {

            var customer = context.Customers.Where(c => c.Id.Equals(customerDetails.Id)).FirstOrDefault();
            if(customer == null) throw new NullReferenceException();
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
