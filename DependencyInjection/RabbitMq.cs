using Consumer.Orders;
using Domain.Events;
using Infrastructure.Config;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ExchangeType = RabbitMQ.Client.ExchangeType;

namespace DependencyInjection;

public static class RabbitMq
{
    public static IServiceCollection SetRabbitMqConfig(this IServiceCollection services, IConfiguration config)
    {
        var rabbitSection = config.GetSection("ExternalServices:RabbitMq");
        services.Configure<RabbitMqConfig>(rabbitSection);

        var settings = rabbitSection.Get<RabbitMqConfig>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<OrderCreatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri($"rabbitmq://{settings?.Host}:{settings?.Port}/"), h =>
                {
                    h.Username(settings.UserName);
                    h.Password(settings.Password);
                });

                cfg.ReceiveEndpoint(nameof(OrderCreated), e =>
                {
                    e.Bind("ecom_cassandra.Domain.Events:OrderCreated", b =>
                    {
                        b.ExchangeType = ExchangeType.Fanout;
                        b.Durable = true;
                    });

                    e.ConfigureConsumer<OrderCreatedConsumer>(context);
                });
            });
        });

        return services;
    }
}