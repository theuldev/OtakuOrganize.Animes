using OtakuOrganize.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Core.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetByIdCustomer(Guid id);
        Task PostCustomer(Customer customer);
        Task PutCustomer(Customer customer);
        Task DeleteCustomer(Guid id);
        Task AddAnimeCustomer(Anime_Customer anime_Customer);
    }
}
