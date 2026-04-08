using Domain.Events;
using ecom_cassandra_notification.Application.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Consumer.Orders;

public class OrderCreatedConsumer(IOrderService orderService, ILogger<OrderCreatedConsumer> logger)
    : IConsumer<OrderCreated>
{
    private readonly IOrderService _orderService = orderService;
    private readonly ILogger<OrderCreatedConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        var response = await _orderService.SendNotificationAsync(context.Message);

        if (response.IsSuccess)
            _logger.LogInformation(string.Join(", ", response.Messages));
        else
            _logger.LogError(string.Join(Environment.NewLine, response.ErrorMessages));
    }
}