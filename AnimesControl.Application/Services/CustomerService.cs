using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Application.Common.Interfaces.Services;
using AnimesControl.Core.Entities;
using System.Net.Http.Headers;
using System.Diagnostics;
using AutoMapper;
using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Models.ViewModels;

namespace AnimesControl.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository repository;
        private readonly IMapper mapper;
        public CustomerService(ICustomerRepository _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }


        public void DeleteCustomer(int id)
        {
            var customer = repository.GetByIdCustomer(id);
            if (customer == null) throw new NullReferenceException();
            repository.DeleteCustomer(customer);
        }

        public CustomerViewModel GetByIdCustomer(int id)
        {
            if (id == null) throw new NullReferenceException();
            var customer = repository.GetByIdCustomer(id);
            var customerMap = mapper.Map<CustomerViewModel>(customer);
            return customerMap;


        }

        public IEnumerable<CustomerViewModel> GetCustomers()
        {
            IEnumerable<Customer> customers = repository.GetCustomers();
            if (customers.Count() <= 0) throw new NullReferenceException();
            var customersMap = mapper.Map<IEnumerable<CustomerViewModel>>(customers);
            return customersMap;
        }

        public void PostCustomer(CustomerInputModel customer)
        {
            if (customer == null) throw new NullReferenceException();
            var customersMap = mapper.Map<Customer>(customer);
            repository.PostCustomer(customersMap);
        }

        public void PutCustomer(int id, CustomerInputModel customer)
        {
            if (id == null || customer == null) throw new NullReferenceException();
            var customersMap = mapper.Map<Customer>(customer);
            repository.PutCustomer(customersMap);
        }
    }
}