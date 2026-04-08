using Domain.Interfaces.Repositories;
using ecom_cassandra_notification.Application.Interfaces;
using ecom_cassandra_notification.Application.Services;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection;

public static class IocConfig
{
    public static IServiceCollection SetIocConfig(this IServiceCollection services)
    {
        #region Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        #endregion

        #region Services
        services.AddTransient<IOrderService, OrderService>();
        #endregion

        return services;
    }
}