using AnimesControl.Application.Common.Interfaces.Services;
using AnimesControl.Application.Mapper;
using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Services;
using AnimesControl.Core.Exceptions;
using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Infra.Caching;
using AnimesControl.Infra.Context;
using AnimesControl.Infra.Repositories;
using AnimesControl.Tests.Persisitence.ContextTest;
using AutoFixture;
using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AnimesControl.Tests.Application.Services;

public class CustomerServiceTests
{
    

    public void ValidCustomer_PostIsCalled_ReturnCustomerModelAddInDatabase()
    {
        var customerInputModel = new Fixture().Create<CustomerInputModel>();

        using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
        {
            var customerRepository = new CustomerRepository(context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomerProfile());

            });
            var mapper = config.CreateMapper();
            var cachingMock = new Mock<ICachingService>();
            var securityService = new SecurityService();
            var customerService = new CustomerService(customerRepository, mapper, cachingMock.Object, securityService);

            customerInputModel.Gender = 1;

            Assert.NotNull(customerInputModel.Name);

            Assert.NotNull(customerInputModel.LastName);
            Assert.NotNull(customerInputModel.Phone);
            Assert.NotEqual(0, customerInputModel.Gender);
            customerService.PostCustomer(customerInputModel);

            var customerContext = context.Customers.AsNoTracking().Where(c => c.Id == customerInputModel.Id).FirstOrDefault();
            Assert.Equal(customerInputModel.Name, customerContext.Name);
            Assert.Equal(customerInputModel.Gender, customerContext.Gender);
            Assert.Equal(customerInputModel.LastName, customerContext.LastName);
            Assert.Equal(customerInputModel.Phone, customerContext.Phone);
            Assert.Equal(customerInputModel.Birthdate, customerContext.Birthdate);

        }
    }
    
    public void InvalidCustomer_PostIsCalled_ThrowAnNullReferenceException()
    {
        var customerInputModel = new Fixture().Create<CustomerInputModel>();

        var customerRepositoryMock = new Mock<ICustomerRepository>();
        var mapperMock = new Mock<IMapper>();
        var cachingMock = new Mock<ICachingService>();
        var securityServiceMock = new Mock<ISecurityService>();
        var customerService = new CustomerService(customerRepositoryMock.Object, mapperMock.Object, cachingMock.Object, securityServiceMock.Object);

        customerInputModel = null;

        var ex = Assert.Throws<ArgumentNullException>(() => customerService.PostCustomer(customerInputModel));
        Assert.Contains("Value cannot be null.", ex.Message);


    }
    
    public void ValidCustomer_GetIsCalled_ReturnCustomerModel()
    {
        var customerInputModel = new Fixture().Create<CustomerInputModel>();

        using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
        {
            var customerRepository = new CustomerRepository(context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomerProfile());

            });
            var mapper = config.CreateMapper();
            var cachingMock = new Mock<ICachingService>();
            var securityService = new SecurityService();
            var customerService = new CustomerService(customerRepository, mapper, cachingMock.Object, securityService);

            customerInputModel.Gender = 1;

            Assert.NotNull(customerInputModel.Name);
            Assert.NotNull(customerInputModel.LastName);
            Assert.NotNull(customerInputModel.Phone);
            Assert.NotEqual(0, customerInputModel.Gender);

            customerService.PostCustomer(customerInputModel);


            var customers = customerService.GetCustomers();
            Assert.Single(customers.Result);


        }
    }
    
    public async void InvalidCustomer_GetIsCalled_ThrowAnNullReferenceException()
    {
        var repositoryMock = new Mock<ICustomerRepository>();
        var mapperMock = new Mock<IMapper>();
        var cachingMock = new Mock<ICachingService>();
        var securityMock = new Mock<ISecurityService>();


        var customerService = new CustomerService(repositoryMock.Object, mapperMock.Object, cachingMock.Object, securityMock.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await customerService.GetCustomers());
    }
    public void ValidCustomer_GetByIdIsCalled_ReturnCustomerModel()
    {
        var customerInputModel = new Fixture().Create<CustomerInputModel>();

        using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
        {

            var customerRepository = new CustomerRepository(context);
            var mapper = new Mock<IMapper>();
            var caching = new Mock<ICachingService>();
            var securityService = new Mock<ISecurityService>();
            var customerService = new CustomerService(customerRepository, mapper.Object, caching.Object, securityService.Object);

            Assert.NotNull(customerInputModel.Name);
            Assert.NotNull(customerInputModel.LastName);
            Assert.NotNull(customerInputModel.Phone);
            Assert.NotEqual(0, customerInputModel.Gender);

            customerService.PostCustomer(customerInputModel);


            var customer = customerService.GetByIdCustomer(customerInputModel.Id);
            Assert.Equal(customerInputModel.Name, customer.Result.Name);
            Assert.Equal(customerInputModel.LastName, customer.Result.LastName);
            Assert.Equal(customerInputModel.Gender, customer.Result.Gender);
            Assert.Equal(customerInputModel.Birthdate, customer.Result.Birthdate);
            Assert.Equal(customerInputModel.Phone, customer.Result.Phone);





        }
    }

    public async void InvalidCustomer_GetByIdIsCalled_ThrowAnArgumentNullException()
    {
        var repositoryMock = new Mock<ICustomerRepository>();
        var mapperMock = new Mock<IMapper>();
        var securityMock = new Mock<ISecurityService>();
        var cachingMock = new Mock<ICachingService>();


        var customerService = new CustomerService(repositoryMock.Object, mapperMock.Object, cachingMock.Object, securityMock.Object);
      
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await customerService.GetByIdCustomer(null));



    }


    public async void InvalidCustomer_GetByIdIsCalled_ThrowAnNullReferenceException()
    {
        var customerInputModel = new Fixture().Create<CustomerInputModel>();
        var repositoryMock = new Mock<ICustomerRepository>();
        var mapperMock = new Mock<IMapper>();
        var securityMock = new Mock<ISecurityService>();
        var cachingMock = new Mock<ICachingService>();


        var customerService = new CustomerService(repositoryMock.Object, mapperMock.Object, cachingMock.Object, securityMock.Object);

        var exception = await Assert.ThrowsAsync<NullReferenceException>(async () => await customerService.GetByIdCustomer(customerInputModel.Id));



    }
    
    public void ValidCustomer_PutIsCalled_CustomerModelMustUpdateInDatabase()
    {
        var customerInputModel = new Fixture().Create<CustomerInputModel>();

        using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
        {

            var customerRepository = new CustomerRepository(context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomerProfile());

            });
            var mapper = config.CreateMapper();
            var caching = new Mock<ICachingService>();
            var securityService = new Mock<ISecurityService>();
            var customerService = new CustomerService(customerRepository, mapper, caching.Object, securityService.Object);

            Assert.NotNull(customerInputModel.Name);
            Assert.NotNull(customerInputModel.LastName);
            Assert.NotNull(customerInputModel.Phone);
            Assert.NotEqual(0, customerInputModel.Gender);

            customerService.PostCustomer(customerInputModel);


            customerInputModel.Name = "Carlinhos";
             customerService.PutCustomer(customerInputModel.Id,customerInputModel);

            var customer = customerService.GetByIdCustomer(customerInputModel.Id);
            Assert.Equal(customerInputModel.Name, customer.Result.Name);
            Assert.Equal(customerInputModel.LastName, customer.Result.LastName);
            Assert.Equal(customerInputModel.Gender, customer.Result.Gender);
            Assert.Equal(customerInputModel.Birthdate, customer.Result.Birthdate);
            Assert.Equal(customerInputModel.Phone, customer.Result.Phone);





        }
    }
    
    public void InvalidCustomer_PutIsCalled_ThrowAnCredentialsNotEqualsException()
    {


        var customerInputModel = new Fixture().Create<CustomerInputModel>();

        using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
        {

            var customerRepository = new CustomerRepository(context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomerProfile());

            });
            var mapper = config.CreateMapper();
            var caching = new Mock<ICachingService>();
            var securityService = new Mock<ISecurityService>();
            var customerService = new CustomerService(customerRepository, mapper, caching.Object, securityService.Object);

         
            customerService.PostCustomer(customerInputModel);


            customerInputModel.Id = Guid.NewGuid();


            var exception = Assert.Throws<CredentialsNotEqualsException>(() => customerService.PutCustomer(Guid.NewGuid(), customerInputModel));


        }
     

    }

    public void InvalidCustomer_PutIsCalled_ThrowAnArgumentNullException()
    {


        var customerInputModel = new Fixture().Create<CustomerInputModel>();
        var repositoryMock = new Mock<ICustomerRepository>();
        var mapperMock = new Mock<IMapper>();
        var securityMock = new Mock<ISecurityService>();
        var cachingMock = new Mock<ICachingService>();


        var customerService = new CustomerService(repositoryMock.Object, mapperMock.Object, cachingMock.Object, securityMock.Object);
        var exception =  Assert.Throws<ArgumentNullException>(() => customerService.PutCustomer(null,customerInputModel));

    }
    
    public void InvalidCustomer_PutIsCalled_ThrowAnNullReferenceException()
    {


        var customerInputModel = new Fixture().Create<CustomerInputModel>();
        var repositoryMock = new Mock<ICustomerRepository>();
        var mapperMock = new Mock<IMapper>();
        var securityMock = new Mock<ISecurityService>();
        var cachingMock = new Mock<ICachingService>();


        var customerService = new CustomerService(repositoryMock.Object, mapperMock.Object, cachingMock.Object, securityMock.Object);
        customerInputModel= null;
        var exception = Assert.Throws<NullReferenceException>(() => customerService.PutCustomer(customerInputModel.Id, customerInputModel));

    }
    public void ValidCustomer_DeleteIsCalled_CustomerModelMustDeleteInDatabase()
    {
        var customerInputModel = new Fixture().Create<CustomerInputModel>();

        using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
        {

            var customerRepository = new CustomerRepository(context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomerProfile());

            });
            var mapper = config.CreateMapper();
            var caching = new Mock<ICachingService>();
            var securityService = new Mock<ISecurityService>();
            var customerService = new CustomerService(customerRepository, mapper, caching.Object, securityService.Object);

            Assert.NotNull(customerInputModel.Name);
            Assert.NotNull(customerInputModel.LastName);
            Assert.NotNull(customerInputModel.Phone);
            Assert.NotEqual(0, customerInputModel.Gender);

            customerService.PostCustomer(customerInputModel);

            customerService.DeleteCustomer(customerInputModel.Id);

            Assert.Empty(context.Customers.ToList());



        }
    }
    
    public async void InvalidCustomer_DeletetIsCalled_ThrowAnArgumentNullException()
    {


        var customerInputModel = new Fixture().Create<CustomerInputModel>();
        var repositoryMock = new Mock<ICustomerRepository>();
        var mapperMock = new Mock<IMapper>();
        var securityMock = new Mock<ISecurityService>();
        var cachingMock = new Mock<ICachingService>();


        var customerService = new CustomerService(repositoryMock.Object, mapperMock.Object, cachingMock.Object, securityMock.Object);
        customerInputModel.Id = Guid.NewGuid();
        var exception = await Assert.ThrowsAsync<ArgumentNullException>( async () => await customerService.DeleteCustomer(customerInputModel.Id));

    }
    
    public async void InvalidCustomer_DeletetIsCalled_ThrowAnNullReferenceException()
    {


        var customerInputModel = new Fixture().Create<CustomerInputModel>();
        var repositoryMock = new Mock<ICustomerRepository>();
        var mapperMock = new Mock<IMapper>();
        var securityMock = new Mock<ISecurityService>();
        var cachingMock = new Mock<ICachingService>();


        var customerService = new CustomerService(repositoryMock.Object, mapperMock.Object, cachingMock.Object, securityMock.Object);

        var exception = await Assert.ThrowsAsync<NullReferenceException>(async () => await customerService.DeleteCustomer(customerInputModel.Id));

    }

}

