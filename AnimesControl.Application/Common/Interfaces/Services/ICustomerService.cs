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
        public IEnumerable<CustomerViewModel> GetCustomers();
        public CustomerViewModel GetByIdCustomer(int id);
        public void PostCustomer(CustomerInputModel customer);
        public void DeleteCustomer(int id);
        public void PutCustomer(int id, CustomerInputModel customer);

    }
}
