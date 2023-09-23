using OtakuOrganize.Application.Mapper;
using OtakuOrganize.Application.Validations;
using OtakuOrganize.Core.Interfaces.Repositories;
using OtakuOrganize.Application.Common.Interfaces.Services;
using OtakuOrganize.Infra.Context;
using OtakuOrganize.Infra.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Text;
using OtakuOrganize.Application.Services;
using OtakuOrganize.Infra.Caching;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OtakuOrganize.Application.Subscribers;
using OtakuOrganize.Infra.Extensions;

namespace OtakuOrganize.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastrutucture(this IServiceCollection services, IConfiguration configuration)
        {
            //add repository classes
            services.AddScoped<IAnimeRepository, AnimeRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAnime_CustomerRepository, Anime_CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //add context class
            services.AddDbContext<AnimeContext>();


            //add redis service

            services.AddStackExchangeRedisCache(o =>
            {
                o.InstanceName = "instance";
                o.Configuration = "localhost:6379";
            });
            services.AddScoped<ICachingService, CachingService>();


            //add consul 
            services.AddConsulConfig(configuration);
            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //add services
            services.AddScoped<IAnimeService, AnimeService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAnime_CustomerService, Anime_CustomerService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();


            //add mapping service
            services.AddAutoMapper(typeof(CustomerProfile));
            services.AddAutoMapper(typeof(AnimeProfile));
            services.AddAutoMapper(typeof(UserProfile));
            //add validation services
            services
                .AddValidatorsFromAssemblyContaining(typeof(AnimeValidator)).AddFluentValidationAutoValidation();

            services
                .AddValidatorsFromAssemblyContaining(typeof(CustomerValidator)).AddFluentValidationAutoValidation();

            services
                .AddValidatorsFromAssemblyContaining(typeof(Anime_CustomerService)).AddFluentValidationAutoValidation();


            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"]))

                };
            });

            services.AddAuthorization();

            return services;

        }
        public static IServiceCollection AddSubscribers( this IServiceCollection services)
        {
            services.AddHostedService<CustomerCreatedSubscriber>();
            return services;
        }
    }
}
