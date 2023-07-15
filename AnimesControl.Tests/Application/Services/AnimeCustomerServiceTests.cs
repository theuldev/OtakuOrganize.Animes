using AnimesControl.Application.Common.Interfaces.Services;
using AnimesControl.Application.Mapper;
using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Services;
using AnimesControl.Infra.Context;
using AnimesControl.Infra.Repositories;
using AnimesControl.Tests.Persisitence.ContextTest;
using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimesControl.Core.Entities;
using AnimesControl.Infra.Caching;
using AnimesControl.Core.Interfaces.Repostories;

namespace AnimesControl.Tests.Application.Services
{
    public class AnimeCustomerServiceTests
    {

        public async void AnimeCustomerValid_AddAnimeCustomerIsCalled_ModelMustAddInDatabase()
        {

            var animeModel = new Fixture().Create<AnimeInputModel>();
            var customerModel = new Fixture().Create<CustomerInputModel>();

            using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
            {
                var customerRepository = new CustomerRepository(context);
                var animeRepository = new AnimeRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AnimeProfile());
                    cfg.AddProfile(new CustomerProfile());
                    cfg.AddProfile(new Anime_CustomerProfile());

                });
                var mapper = config.CreateMapper();
                var cachingMock = new Mock<ICachingService>();
                var animeService = new AnimeService(animeRepository, mapper, cachingMock.Object);


                animeService.PostAnime(animeModel);

                var securityService = new SecurityService();
                var customerService = new CustomerService(customerRepository, mapper, cachingMock.Object, securityService);

                customerService.PostCustomer(customerModel);

                var animecustomerrepository = new Anime_CustomerRepository(context);

                var animeCustomerModel = new Anime_CustomerInputModel(animeModel.Id, customerModel.Id);

                var service = new Anime_CustomerService(customerService, animeService, animecustomerrepository, mapper);

                await service.AddAnimeCustomer(animeCustomerModel);

                var animeContext = context.Anime_Customer.AsNoTracking().Where(c => c.AnimeId == animeCustomerModel.AnimeId && animeCustomerModel.CustomerId.Equals(c.CustomerId)).FirstOrDefault();
                Assert.NotNull(animeContext);
                Assert.Single(context.Anime_Customer.ToList());


            }
        }
        public async void AnimeCustomerInvalid_AddAnimeCustomerIsCalled_ThrowNewNullReferenceException()
        {
            var animeCustomerModel = new Fixture().Create<Anime_CustomerInputModel>();
            var animeServiceMock = new Mock<IAnimeService>();
            var customerServiceMock = new Mock<ICustomerService>();
            var animeCustomerRepositoryMock = new Mock<IAnime_CustomerRepository>();
            var mapperMock = new Mock<IMapper>();

            var service = new Anime_CustomerService(customerServiceMock.Object, animeServiceMock.Object, animeCustomerRepositoryMock.Object, mapperMock.Object);



            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => service.AddAnimeCustomer(animeCustomerModel));



        }
        public async void AnimeCustomerValid_GetCustomerWithAnimeIdÌsCalled_ReturnAnimeCustomerModel()
        {
            var animeModel = new Fixture().Create<AnimeInputModel>();
            var customerModel = new Fixture().Create<CustomerInputModel>();

            using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
            {
                var customerRepository = new CustomerRepository(context);
                var animeRepository = new AnimeRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AnimeProfile());
                    cfg.AddProfile(new CustomerProfile());
                    cfg.AddProfile(new Anime_CustomerProfile());

                });
                var mapper = config.CreateMapper();
                var cachingMock = new Mock<ICachingService>();
                var animeService = new AnimeService(animeRepository, mapper, cachingMock.Object);


                animeService.PostAnime(animeModel);

                var securityService = new SecurityService();
                var customerService = new CustomerService(customerRepository, mapper, cachingMock.Object, securityService);

                customerService.PostCustomer(customerModel);

                var animecustomerrepository = new Anime_CustomerRepository(context);

                var animeCustomerModel = new Anime_CustomerInputModel(animeModel.Id, customerModel.Id);

                var service = new Anime_CustomerService(customerService, animeService, animecustomerrepository, mapper);

                await service.AddAnimeCustomer(animeCustomerModel);


                var response = await service.GetCustomerWithAnimeId(animeCustomerModel.AnimeId);

                Assert.NotNull(response);

                Assert.Single(response);
            }
        }
      
        public async void AnimeCustomerInvalid_GetCustomerWithAnimeIdÌsCalled_ThrowNullReferenceException()
        {
            var animeCustomerModel = new Fixture().Create<Anime_CustomerInputModel>();
            var animeServiceMock = new Mock<IAnimeService>();
            var customerServiceMock = new Mock<ICustomerService>();
            var animeCustomerRepositoryMock = new Mock<IAnime_CustomerRepository>();
            var mapperMock = new Mock<IMapper>();

            var service = new Anime_CustomerService(customerServiceMock.Object, animeServiceMock.Object, animeCustomerRepositoryMock.Object, mapperMock.Object);



            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => service.GetCustomerWithAnimeId(animeCustomerModel.AnimeId));
        }

        public async void AnimeCustomerInvalid_GetCustomerWithAnimeIdÌsCalled_ThrowArgumentNullException()
        {
        
            var animeServiceMock = new Mock<IAnimeService>();
            var customerServiceMock = new Mock<ICustomerService>();
            var animeCustomerRepositoryMock = new Mock<IAnime_CustomerRepository>();
            var mapperMock = new Mock<IMapper>();

            var service = new Anime_CustomerService(customerServiceMock.Object, animeServiceMock.Object, animeCustomerRepositoryMock.Object, mapperMock.Object);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetCustomerWithAnimeId(null));
        }
        public async void AnimeCustomerValid_GetAnimeWithCustomerIdÌsCalled_ReturnAnimeCustomerModel()
        {
            var animeModel = new Fixture().Create<AnimeInputModel>();
            var customerModel = new Fixture().Create<CustomerInputModel>();

            using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
            {
                var customerRepository = new CustomerRepository(context);
                var animeRepository = new AnimeRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AnimeProfile());
                    cfg.AddProfile(new CustomerProfile());
                    cfg.AddProfile(new Anime_CustomerProfile());

                });
                var mapper = config.CreateMapper();
                var cachingMock = new Mock<ICachingService>();
                var animeService = new AnimeService(animeRepository, mapper, cachingMock.Object);


                animeService.PostAnime(animeModel);

                var securityService = new SecurityService();
                var customerService = new CustomerService(customerRepository, mapper, cachingMock.Object, securityService);

                customerService.PostCustomer(customerModel);

                var animecustomerrepository = new Anime_CustomerRepository(context);

                var animeCustomerModel = new Anime_CustomerInputModel(animeModel.Id, customerModel.Id);

                var service = new Anime_CustomerService(customerService, animeService, animecustomerrepository, mapper);

                await service.AddAnimeCustomer(animeCustomerModel);


                var response = await service.GetAnimeWithCustomerId(animeCustomerModel.CustomerId);

                Assert.NotNull(response);

                Assert.Single(response);
            }
   
        }
        public async void AnimeCustomerInvalid_GetAnimeWithCustomerIdÌsCalled_ThrowNullReferenceException()
        {
            var animeCustomerModel = new Fixture().Create<Anime_CustomerInputModel>();
            var animeServiceMock = new Mock<IAnimeService>();
            var customerServiceMock = new Mock<ICustomerService>();
            var animeCustomerRepositoryMock = new Mock<IAnime_CustomerRepository>();
            var mapperMock = new Mock<IMapper>();

            var service = new Anime_CustomerService(customerServiceMock.Object, animeServiceMock.Object, animeCustomerRepositoryMock.Object, mapperMock.Object);



            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => service.GetAnimeWithCustomerId(animeCustomerModel.CustomerId));
        }
        public async void AnimeCustomerInvalid_GetAnimeWithCustomerIdÌsCalled_ThrowArgumentNullException()
        {

            var animeServiceMock = new Mock<IAnimeService>();
            var customerServiceMock = new Mock<ICustomerService>();
            var animeCustomerRepositoryMock = new Mock<IAnime_CustomerRepository>();
            var mapperMock = new Mock<IMapper>();

            var service = new Anime_CustomerService(customerServiceMock.Object, animeServiceMock.Object, animeCustomerRepositoryMock.Object, mapperMock.Object);



            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetAnimeWithCustomerId(null));
        }
        public async void AnimeCustomerValid_RemoveAnimeCustomer_RemoveAnimeCustomer()
         {
            var animeModel = new Fixture().Create<AnimeInputModel>();
            var customerModel = new Fixture().Create<CustomerInputModel>();

            using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
            {
                var customerRepository = new CustomerRepository(context);
                var animeRepository = new AnimeRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AnimeProfile());
                    cfg.AddProfile(new CustomerProfile());
                    cfg.AddProfile(new Anime_CustomerProfile());

                });
                var mapper = config.CreateMapper();
                var cachingMock = new Mock<ICachingService>();
                var animeService = new AnimeService(animeRepository, mapper, cachingMock.Object);


                animeService.PostAnime(animeModel);

                var securityService = new SecurityService();
                var customerService = new CustomerService(customerRepository, mapper, cachingMock.Object, securityService);

                customerService.PostCustomer(customerModel);

                var animecustomerrepository = new Anime_CustomerRepository(context);

                var animeCustomerModel = new Anime_CustomerInputModel(animeModel.Id, customerModel.Id);

                var service = new Anime_CustomerService(customerService, animeService, animecustomerrepository, mapper);

                await service.AddAnimeCustomer(animeCustomerModel);

                service.RemoveAnimeCustomer(animeCustomerModel);

                Assert.Empty(context.Anime_Customer.ToList());


            }

        }

        public async void AnimeCustomerInvalid_RemoveAnimeCustomerÌsCalled_ThrowNullReferenceException()
        {
            var animeCustomerModel = new Fixture().Create<Anime_CustomerInputModel>();
            var animeServiceMock = new Mock<IAnimeService>();
            var customerServiceMock = new Mock<ICustomerService>();
            var animeCustomerRepositoryMock = new Mock<IAnime_CustomerRepository>();
            var mapperMock = new Mock<IMapper>();

            var service = new Anime_CustomerService(customerServiceMock.Object, animeServiceMock.Object, animeCustomerRepositoryMock.Object, mapperMock.Object);




            var exception = Assert.Throws<NullReferenceException>(() => service.RemoveAnimeCustomer(null));
        }
    }
}
