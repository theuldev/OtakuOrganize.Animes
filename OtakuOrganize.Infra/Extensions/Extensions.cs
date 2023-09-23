using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace OtakuOrganize.Infra.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {

               var address = configuration.GetSection("Consul:Host").Value ?? throw new NullReferenceException();
                consulConfig.Address = new Uri(address);

            }));

            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app){
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            var registration = new AgentServiceRegistration
            {
                ID = $"anime-service-{Guid.NewGuid()}",
                Name = "AnimeServices",
                Address = "localhost",
                Port = 5161
            };

            try
            {
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
                consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

                lifetime.ApplicationStopping.Register(() =>
                {
                    consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
                    Console.WriteLine($"Service with Id {registration.ID} and Name {registration.Name} deregistered in Consul");


                });

                Console.WriteLine($"Service with Id {registration.ID} and Name {registration.Name} registered in Consul");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return app;


        }
    }
}