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
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetByIdCustomer(int id);
        void PostCustomer(Customer customer);
        void PutCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        void AddAnimeCustomer(Anime_Customer anime_Customer);
        bool ExistsUsername(string username);
        bool ExistsEmail(string email);
    }
}
