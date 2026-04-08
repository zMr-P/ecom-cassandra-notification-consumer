using Domain.Events;
using MrP.FluentResult.Artifacts;

namespace ecom_cassandra_notification.Application.Interfaces;

public interface IOrderService
{
    Task<Result> SendNotificationAsync(OrderCreated orderCreatedEvent);
}