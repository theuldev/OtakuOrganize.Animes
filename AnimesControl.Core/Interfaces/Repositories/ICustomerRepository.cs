using AnimesControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Core.Interfaces.Repostories
{
    public interface ICustomerRepository
    {
        public List<Customer> GetCustomers();
        public Customer GetByIdCustomer(int id);
        public void PostCustomer(Customer customer);
        public void PutCustomer(Customer customer);
        public void DeleteCustomer(Customer customer);
        public void AddAnimeCustomer(Customer customer, Anime anime);
    }
}
