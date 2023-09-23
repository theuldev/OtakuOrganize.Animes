using OtakuOrganize.Application.Common.Interfaces.Services;
using OtakuOrganize.Application.Mapper;
using OtakuOrganize.Application.Models.InputModels;
using OtakuOrganize.Application.Services;
using OtakuOrganize.Core.Entities;
using OtakuOrganize.Core.Exceptions;
using OtakuOrganize.Core.Interfaces.Repositories;
using OtakuOrganize.Infra.Caching;
using OtakuOrganize.Infra.Context;
using OtakuOrganize.Infra.Repositories;
using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OtakuOrganize.Tests.Persisitence.ContextTest;
using Microsoft.Extensions.Configuration;

namespace OtakuOrganize.Tests.Application.Services;

public class AnimeServiceTests
{
    [Fact]
    public async void ValidAnime_PostIsCalled_AnimeInputModelMustAddInDatabase()
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

        
            Assert.NotNull(customerInputModel.Phone);
            Assert.NotEqual(0, customerInputModel.Gender);
            customerService.PostCustomer(customerInputModel);

            var customerContext = context.Customers.AsNoTracking().Where(c => c.Id == customerInputModel.Id).FirstOrDefault();
            Assert.Equal(customerInputModel.Name, customerContext.Name);
            Assert.Equal(customerInputModel.Gender, customerContext.Gender);
            Assert.Equal(customerInputModel.Phone, customerContext.Phone);
            Assert.Equal(customerInputModel.Birthdate, customerContext.Birthdate);

        }


    }

   
    public void InvalidAnime_PostIsCalled_ThrowAnArgumentNullException()
    {

        var animeInputModel = new Fixture().Create<AnimeInputModel>();

        var animeRepositoryMock = new Mock<IAnimeRepository>();
        var mapperMock = new Mock<IMapper>();
        var cachingMock = new Mock<ICachingService>();
        var animeService = new AnimeService(animeRepositoryMock.Object, mapperMock.Object, cachingMock.Object);

        animeInputModel = null;
        var exception = Assert.ThrowsAsync<ArgumentNullException>(() => animeService.PostAnime(animeInputModel));


    }

    public void ValidAnime_PutIsCalled_AnimeModelMustUpdateInDatabase()
    {
        var id = Guid.NewGuid();
        var animeInputModel = new Fixture().Create<AnimeInputModel>();
        animeInputModel.Id = id;
        animeInputModel.Title = "Chainsaw Man";
        animeInputModel.Details = "Motoserra faz ran dan";
        animeInputModel.Category = 1;



        using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
        {
            var animeRepository = new AnimeRepository(context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AnimeProfile());
            });
            var mapper = config.CreateMapper();
            var caching = new Mock<ICachingService>();


            var animeService = new AnimeService(animeRepository, mapper, caching.Object);

            Assert.NotNull(animeInputModel.Title);
            Assert.NotNull(animeInputModel.Details);
            Assert.NotEmpty(animeInputModel.Title);
            Assert.NotEmpty(animeInputModel.Details);
            Assert.NotEqual(0, animeInputModel.Category);

            animeService.PostAnime(animeInputModel);

            animeInputModel.Title = "One piece";
            animeInputModel.Details = "Dos Piratas";
            animeInputModel.Category = 2;



            animeService.PutAnime(animeInputModel.Id, animeInputModel);
            var animeContext = context.Anime.AsNoTracking().Where(c => c.Id == animeInputModel.Id).FirstOrDefault();

            Assert.Equal(animeInputModel.Title, animeContext.Title);
            Assert.Equal(animeInputModel.Details, animeContext.Details);
            Assert.Equal(animeInputModel.PostAt, animeContext.PostAt);
            Assert.Equal(animeInputModel.Id, animeContext.Id);
            Assert.Equal(animeInputModel.ReleaseDate, animeContext.ReleaseDate);
            Assert.Equal(animeInputModel.Category, animeContext.Category);
            Assert.Equal(1, context.Anime.Count());


        }

    }

    public void InvalidAnime_PutIsCalled_ThrowAnNullReferenceException()
    {
        var animeInputModel = new Fixture().Create<AnimeInputModel>();
        animeInputModel.Id = Guid.NewGuid();
        animeInputModel.Title = "Chainsaw Man";
        animeInputModel.Details = "Motoserra faz ran dan";
        animeInputModel.Category = 1;



        using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
        {
            var animeRepository = new AnimeRepository(context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AnimeProfile());
            });
            var mapper = config.CreateMapper();
            var caching = new Mock<ICachingService>();


            var animeService = new AnimeService(animeRepository, mapper, caching.Object);

            animeService.PostAnime(animeInputModel);

            animeInputModel.Id = Guid.NewGuid();
            animeInputModel.Title = "One piece";
            animeInputModel.Details = "Dos Piratas";
            animeInputModel.Category = 2;



            Assert.ThrowsAsync<CredentialsNotEqualsException>(() => animeService.PutAnime(Guid.NewGuid(), animeInputModel));
        }
    }


    public void InvalidAnime_PutIsCalled_ThrowCredentialsNotEqualsException()
    {

        var animeInputModel = new Fixture().Create<AnimeInputModel>();
        var id = Guid.NewGuid();
        animeInputModel.Id = Guid.NewGuid();
        animeInputModel.Title = "Chainsaw Man";
        animeInputModel.Details = "Motoserra faz ran dan";
        animeInputModel.Category = 1;



        using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
        {
            var animeRepository = new AnimeRepository(context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AnimeProfile());
            });
            var mapper = config.CreateMapper();
            var caching = new Mock<ICachingService>();


            var animeService = new AnimeService(animeRepository, mapper, caching.Object);

            animeService.PostAnime(animeInputModel);

            animeInputModel.Id = id;
            animeInputModel.Title = "One piece";
            animeInputModel.Details = "Dos Piratas";
            animeInputModel.Category = 2;


            Assert.ThrowsAsync<NullReferenceException>(() => animeService.PutAnime(id, animeInputModel));
        }
    }

    public void ValidAnime_DeleteIsCalled_AnimeModelMustDeletedInDatabase()
    {


        var anime = new AnimeInputModel();
        anime.Id = Guid.NewGuid();
        anime.Title = "One piece";
        anime.Details = "Dos Piratas";
        anime.Category = 2;

        var options = new DbContextOptionsBuilder<AnimeContext>().UseInMemoryDatabase(databaseName: "AnimeDatabase").Options;
        using (var context = new AnimeContext(options))
        {
            var animeRepository = new AnimeRepository(context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AnimeProfile());
            });
            var mapper = config.CreateMapper();
            var caching = new Mock<ICachingService>();


            var animeService = new AnimeService(animeRepository, mapper, caching.Object);

            animeService.PostAnime(anime);

            animeService.DeleteAnime(anime.Id);

            Assert.Empty(context.Anime.ToList());


        }
    }

    public void InvalidAnime_DeleteIsCalled_ThrowNullReferenceException()
    {

        var animeInputModel = new Fixture().Create<AnimeInputModel>();
        animeInputModel = null;



        using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
        {
            var animeRepository = new AnimeRepository(context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AnimeProfile());
            });
            var mapper = config.CreateMapper();
            var caching = new Mock<ICachingService>();


            var animeService = new AnimeService(animeRepository, mapper, caching.Object);


            Assert.ThrowsAsync<NullReferenceException>(() => animeService.DeleteAnime(animeInputModel.Id));
        }

    }

    public void ValidAnime_GetIsCalled_ReturnValidAnimeViewModel()
    {
        var animeInputModel = new Fixture().Create<AnimeInputModel>();





        using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
        {
            var animeRepository = new AnimeRepository(context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AnimeProfile());

            });
            var mapper = config.CreateMapper();
            var cachingMock = new Mock<ICachingService>();
            var animeService = new AnimeService(animeRepository, mapper, cachingMock.Object);

            animeInputModel.Category = 1;
            animeService.PostAnime(animeInputModel);

            Assert.NotNull(animeInputModel.Title);
            Assert.NotNull(animeInputModel.Details);
            Assert.NotEmpty(animeInputModel.Title);
            Assert.NotEmpty(animeInputModel.Details);
            Assert.NotEqual(0, animeInputModel.Category);

            var animes = animeService.GetAnimes();

            Assert.Single(animes.Result);


        }
    }

    public async Task InvalidAnime_GetIsCalled_ThrowNullReferenceException()
    {
        var animeRepositoryMock = new Mock<IAnimeRepository>();
        var mapperMock = new Mock<IMapper>();
        var cachingMock = new Mock<ICachingService>();
        var animeService = new AnimeService(animeRepositoryMock.Object, mapperMock.Object, cachingMock.Object);



        var exAnime = await Assert.ThrowsAsync<NullReferenceException>(() => animeService.GetAnimes());
    }

    public void ValidAnime_GetByIdIsCalled_ReturnValidAnimeViewModel()
    {
        var animeInputModel = new Fixture().Create<AnimeInputModel>();
        animeInputModel.Id = Guid.NewGuid();
        animeInputModel.Title = "Chainsaw Man";
        animeInputModel.Details = "Motoserra faz ran dan";
        animeInputModel.Category = 1;



        using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
        {
            var animeRepository = new AnimeRepository(context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AnimeProfile());
            });
            var mapper = config.CreateMapper();
            var caching = new Mock<ICachingService>();


            var animeService = new AnimeService(animeRepository, mapper, caching.Object);

            Assert.NotNull(animeInputModel.Title);
            Assert.NotNull(animeInputModel.Details);
            Assert.NotEmpty(animeInputModel.Title);
            Assert.NotEmpty(animeInputModel.Details);
            Assert.NotEqual(0, animeInputModel.Category);

            animeService.PostAnime(animeInputModel);



            var anime = animeService.GetByIdAnimeDetails(animeInputModel.Id);


            Assert.Equal(animeInputModel.Title, anime.Result.Title);
            Assert.Equal(animeInputModel.Details, anime.Result.Details);
            Assert.Equal(animeInputModel.PostAt, anime.Result.PostAt);
            Assert.Equal(animeInputModel.Id, anime.Result.Id);
            Assert.Equal(animeInputModel.ReleaseDate, anime.Result.ReleaseDate);
            Assert.Equal(animeInputModel.Category, anime.Result.Category);
            Assert.Equal(1, context.Anime.Count());

        }


    }

    public void InvalidAnime_GetByIdIsCalled_ThrowAnArgumentNullException()
    {
        var animeInputModel = new Fixture().Create<AnimeInputModel>();
        animeInputModel = null;



        using (var context = new AnimeContext(DbContextTest.CreateDbContextOptions()))
        {
            var animeRepository = new AnimeRepository(context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AnimeProfile());
            });
            var mapper = config.CreateMapper();
            var caching = new Mock<ICachingService>();


            var animeService = new AnimeService(animeRepository, mapper, caching.Object);

            var exAnime = Assert.ThrowsAsync<NullReferenceException>(() => animeService.GetByIdAnimeDetails(animeInputModel.Id));


        }


    }
   
}
   