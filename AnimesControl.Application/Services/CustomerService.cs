using AnimesControl.Application.Common.Interfaces.Services;
using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Models.ViewModels;
using AnimesControl.Core.Entities;
using AnimesControl.Core.Exceptions;
using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Infra.Caching;
using AutoMapper;
using Newtonsoft.Json;

namespace AnimesControl.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository repository;
        private readonly IMapper mapper;
        private readonly ICachingService cachingService;
        private readonly ISecurityService securityService;
        public CustomerService(ICustomerRepository _repository, IMapper _mapper, ICachingService _cachingService, ISecurityService _securityService)
        {
            repository = _repository;
            mapper = _mapper;
            cachingService = _cachingService;
            securityService = _securityService;
        }

        public async Task DeleteCustomer(Guid id)
        {
            if (id == null) throw new ArgumentNullException();
            var customer = await repository.GetByIdCustomer(id);
            repository.DeleteCustomer(customer);
        }

        public async Task<CustomerViewModel> GetByIdCustomer(Guid id)
        {

            if (id == null) throw new ArgumentNullException();

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
                if (customer == null) throw new NullReferenceException();
            }
            var customerMap = mapper.Map<CustomerViewModel>(customer);
            return customerMap;


        }

        public async Task<IEnumerable<CustomerViewModel>> GetCustomers()
        {
            IEnumerable<Customer> customers = await repository.GetCustomers();
            if (customers.Count() <= 0) throw new ArgumentNullException();
            var customersMap = mapper.Map<IEnumerable<CustomerViewModel>>(customers);
            return customersMap.OrderBy(c => c.Id);
        }

        public void PostCustomer(CustomerInputModel customer)
        {
            if (customer == null) throw new ArgumentNullException();

            var customerMap = mapper.Map<Customer>(customer);
            repository.PostCustomer(customerMap);
        }

        public void PutCustomer(Guid id, CustomerInputModel customer)
        {
            if (id.Equals(null) || customer == null) throw new ArgumentNullException();


            if (!customer.Id.Equals(id)) throw new CredentialsNotEqualsException();


            var customersMap = mapper.Map<Customer>(customer);
            repository.PutCustomer(customersMap);
        }


    }
}