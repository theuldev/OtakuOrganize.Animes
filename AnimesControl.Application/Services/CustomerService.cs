using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Application.Common.Interfaces.Services;
using AnimesControl.Core.Entities;
using System.Net.Http.Headers;
using System.Diagnostics;
using AutoMapper;
using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Models.ViewModels;
using AnimesControl.Infra.Caching;
using Newtonsoft.Json;
using AnimesControl.Core.Exceptions;
using System.Security.Cryptography;

namespace AnimesControl.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository repository;
        private readonly IMapper mapper;
        private readonly ICachingService cachingService;
        private readonly ISecurityService securityService;
        public CustomerService(ICustomerRepository _repository, IMapper _mapper, ICachingService _cachingService,ISecurityService _securityService)
        {
            repository = _repository;
            mapper = _mapper;
            cachingService = _cachingService;
            securityService = _securityService;
        }


        public async void DeleteCustomer(int id)
        {
            var customer = await repository.GetByIdCustomer(id);
            if (customer == null) throw new NullReferenceException();
            repository.DeleteCustomer(customer);
        }

        public async Task<CustomerViewModel> GetByIdCustomer(int id)
        {

            if (id == null) throw new NullReferenceException();

            var cacheValue = await cachingService.GetAsync(id.ToString());
            Customer customer;
            if (!string.IsNullOrWhiteSpace(cacheValue))
            {
                customer = JsonConvert.DeserializeObject<Customer>(cacheValue);
                await cachingService.SetAsync(id.ToString(), JsonConvert.SerializeObject(customer));
            }
            else
            {
                customer = await repository.GetByIdCustomer(id);
            }
            var customerMap = mapper.Map<CustomerViewModel>(customer);
            return customerMap;


        }

        public async Task<IEnumerable<CustomerViewModel>> GetCustomers()
        {
            IEnumerable<Customer> customers = await repository.GetCustomers();
            if (customers.Count() <= 0) throw new NullReferenceException();
            var customersMap = mapper.Map<IEnumerable<CustomerViewModel>>(customers);
            return customersMap.OrderBy(c => c.Id);
        }

        public async void PostCustomer(CustomerInputModel customer)
        {
            if (customer == null) throw new NullReferenceException();

            var userexist = repository.ExistsUsername(customer.Username);
            if (userexist == true) throw new UsernameAlreadyRegisteredException();

            var emailexist = repository.ExistsEmail(customer.Email);
            if (emailexist == true) throw new EmailAlreadyRegisteredException();

       

            var customerMap = mapper.Map<Customer>(customer);
            customerMap.Password = await securityService.EncryptPassword(customer.Password);

            repository.PostCustomer(customerMap);
        }

        public void PutCustomer(int id, CustomerInputModel customer)
        {
            if (id == null || customer == null) throw new NullReferenceException();

            var emailexist = repository.ExistsEmail(customer.Email);
            if (emailexist == true) throw new EmailAlreadyRegisteredException();

            var customersMap = mapper.Map<Customer>(customer);
            repository.PutCustomer(customersMap);
        }
      
    }
}