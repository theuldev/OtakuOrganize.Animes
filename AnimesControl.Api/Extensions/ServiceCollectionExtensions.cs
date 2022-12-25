using AnimesControl .Application.Mapper;
using AnimesControl.Application.Validations;
using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Application.Common.Interfaces.Services;
using AnimesControl.Domain.Services;
using AnimesControl.Infra.Context;
using AnimesControl.Infra.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Infra.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastrutucture(this IServiceCollection services)
        {
            //add repository classes
            services.AddScoped<IAnimeRepository, AnimeRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAnime_CustomerRepository, Anime_CustomerRepository>();

            //add context class
            services.AddDbContext<AnimeContext>();
         
            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //add services
            services.AddScoped<IAnimeService, AnimeService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAnime_CustomerService, Anime_CustomerService>();

            //add mapping service
            services.AddAutoMapper(typeof(CustomerProfile));
            services.AddAutoMapper(typeof(AnimeProfile));

            //add validation services
            services
                .AddValidatorsFromAssemblyContaining(typeof(AnimeValidator)).AddFluentValidationAutoValidation();

            services
                .AddValidatorsFromAssemblyContaining(typeof(CustomerValidator)).AddFluentValidationAutoValidation();

            services
                .AddValidatorsFromAssemblyContaining(typeof(Anime_CustomerService)).AddFluentValidationAutoValidation();

            return services;

        }
    }
}
