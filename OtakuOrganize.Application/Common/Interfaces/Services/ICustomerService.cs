using OtakuOrganize.Application.Models.InputModels;
using OtakuOrganize.Application.Models.ViewModels;
using OtakuOrganize.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Application.Common.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerViewModel>> GetCustomers();
        Task<CustomerViewModel> GetByIdCustomer(Guid id);
        Task PostCustomer(CustomerInputModel customer);
        Task DeleteCustomer(Guid id);
        Task PutCustomer(Guid id, CustomerInputModel customer);

    }
}
