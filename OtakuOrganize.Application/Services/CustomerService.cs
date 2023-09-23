using AutoMapper;
using OtakuOrganize.Application.Common.Interfaces.Services;
using OtakuOrganize.Application.Models.InputModels;
using OtakuOrganize.Application.Models.ViewModels;
using OtakuOrganize.Core.Entities;
using OtakuOrganize.Core.Exceptions;
using OtakuOrganize.Core.Interfaces.Repositories;
using OtakuOrganize.Infra.Caching;

namespace OtakuOrganize.Application.Services
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
       
            var customer = await repository.GetByIdCustomer(id);
            await repository.DeleteCustomer(customer.Id);
        }

        public async Task<CustomerViewModel> GetByIdCustomer(Guid id)
        {


            var customer = await repository.GetByIdCustomer(id);

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

        public async Task PostCustomer(CustomerInputModel customer)
        {
            if (customer == null) throw new ArgumentNullException();

            var customerMap = mapper.Map<Customer>(customer);
            await repository.PostCustomer(customerMap);
        }

        public async Task PutCustomer(Guid id, CustomerInputModel customer)
        {
            if (id.Equals(null) || customer == null) throw new ArgumentNullException();


            if (!customer.Id.Equals(id)) throw new CredentialsNotEqualsException();


            var customersMap = mapper.Map<Customer>(customer);
            await repository.PutCustomer(customersMap);
        }


    }
}