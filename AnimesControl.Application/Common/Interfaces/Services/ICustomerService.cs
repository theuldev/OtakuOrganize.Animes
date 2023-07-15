using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Models.ViewModels;
using AnimesControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Application.Common.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerViewModel>> GetCustomers();
        Task<CustomerViewModel> GetByIdCustomer(Guid id);
        void PostCustomer(CustomerInputModel customer);
        Task DeleteCustomer(Guid id);
        void PutCustomer(Guid id, CustomerInputModel customer);

    }
}
