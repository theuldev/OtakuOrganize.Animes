using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OtakuOrganize.Application.Models.InputModels;
using OtakuOrganize.Core.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OtakuOrganize.Application.Subscribers
{
    public class CustomerCreatedSubscriber : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection connection;
        private readonly IModel channel;
        private const string Queue = "customer-service/create-customer";
        private const string Exchange = "customer-service";
        private const string RoutingKey = "create-customer";
        public CustomerCreatedSubscriber(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var connectionFactory = new ConnectionFactory {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            connection = connectionFactory.CreateConnection("customer-service-create-customer-subscriber");
            channel = connection.CreateModel();
            channel.ExchangeDeclare(Exchange, "topic", true);
            channel.QueueDeclare(queue: Queue,
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);
            channel.QueueBind(Queue, "customer-service", RoutingKey);
        }

       
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var byteArray = eventArgs.Body.ToArray();
                var contentArray = Encoding.UTF8.GetString(byteArray);
                var message = JsonConvert.DeserializeObject<CustomerInputModel>(contentArray);
                Console.WriteLine($"Message received with message:{message?.Id}");

                channel.BasicAck(eventArgs.DeliveryTag, false);

            };

            channel.BasicConsume(Queue, false, consumer);
            return Task.CompletedTask;
            
        }
    }
}
