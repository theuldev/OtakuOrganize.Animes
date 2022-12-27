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
        public List<Customer> GetCustomers()
        {
            var customers = context.Customers.ToList();
            return customers;
        }

        public Customer GetByIdCustomer(int id)
        {
           var customer = context.Customers.AsNoTracking().FirstOrDefault(x => x.Id == id);
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
            context.Entry(customer).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            context.Remove(customer);
            context.SaveChanges();

        }

        public void AddAnimeCustomer(Customer customer, Anime anime)
        {
         
            var anime_customer = new Anime_Customer()
            {
                AnimeId = anime.Id,
                CustomerId = customer.Id,
            };
            context.Anime_Customer.Add(anime_customer);
            context.SaveChanges();
        }
    }
}
