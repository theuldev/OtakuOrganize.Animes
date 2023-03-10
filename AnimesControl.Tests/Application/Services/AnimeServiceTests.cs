using AnimesControl.Application.Common.Interfaces.Services;
using AnimesControl.Application.Mapper;
using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Services;
using AnimesControl.Core.Entities;
using AnimesControl.Core.Exceptions;
using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Infra.Caching;
using AnimesControl.Infra.Context;
using AnimesControl.Infra.Repositories;
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
using AnimesControl.Tests.Persisitence.ContextTest;
namespace AnimesControl.Tests.Application.Services;

public class AnimeServiceTests
{
    
    public void ValidAnime_PostIsCalled_AnimeModelMustAddInDatabase()
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

            var animeContext = context.Anime.AsNoTracking().Where(c => c.Id == animeInputModel.Id).FirstOrDefault();
            Assert.Equal(animeInputModel.Title, animeContext.Title);
            Assert.Equal(animeInputModel.Details, animeContext.Details);
            Assert.Equal(animeInputModel.PostAt, animeContext.PostAt);
            Assert.Equal(animeInputModel.ReleaseDate, animeContext.ReleaseDate);
            Assert.Equal(animeInputModel.Category, animeContext.Category);

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
        var exception = Assert.Throws<ArgumentNullException>(() => animeService.PostAnime(animeInputModel));


    }
    
    public void ValidAnime_PutIsCalled_AnimeModelMustUpdateInDatabase()
    {
        var id = new Random().Next();
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
        Random random = new Random();
        animeInputModel.Id = 1;
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

            animeInputModel.Id = 1;
            animeInputModel.Title = "One piece";
            animeInputModel.Details = "Dos Piratas";
            animeInputModel.Category = 2;



            Assert.Throws<CredentialsNotEqualsException>(() => animeService.PutAnime(random.Next(10), animeInputModel));
        }
    }

    
    public void InvalidAnime_PutIsCalled_ThrowCredentialsNotEqualsException()
    {

        var animeInputModel = new Fixture().Create<AnimeInputModel>();
        Random random = new Random();
        var id = random.Next(2, 10);
        animeInputModel.Id = 1;
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


            Assert.Throws<NullReferenceException>(() => animeService.PutAnime(id, animeInputModel));
        }
    }
    [Fact]
    
    public void ValidAnime_DeleteIsCalled_AnimeModelMustDeletedInDatabase()
    {


        var anime = new AnimeInputModel();
        anime.Id = 1;
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


            Assert.Throws<NullReferenceException>(() => animeService.DeleteAnime(animeInputModel.Id));
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
        var id = new Random().Next();
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
