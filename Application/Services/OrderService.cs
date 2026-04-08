using Domain.Events;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Senders;
using Domain.Messages;
using ecom_cassandra_notification.Application.Interfaces;
using MrP.FluentResult.Artifacts;
using MrP.FluentResult.FluentExtensions;

namespace ecom_cassandra_notification.Application.Services;

public class OrderService(
    IEmailSender emailSender,
    IEmailTemplateProvider emailTemplateProvider,
    IUserRepository userRepository) : IOrderService
{
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IEmailTemplateProvider _emailTemplateProvider = emailTemplateProvider;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> SendNotificationAsync(OrderCreated orderCreatedEvent)
    {
        try
        {
            var user = await _userRepository.ReadByIdAsync(orderCreatedEvent.UserId);

            if (user == null)
                return new Result(false)
                    .AddErrorMessage(ErrorMessages.UserNotFound(orderCreatedEvent.UserId));

            var template = _emailTemplateProvider.ReadTemplate(nameof(OrderCreated));

            var templateBody = template
                .Replace("{{UserName}}", user.Name)
                .Replace("{{OrderNumber}}", orderCreatedEvent.OrderId.ToString())
                .Replace("{{OrderDate}}", orderCreatedEvent.CreatedAt.ToString("dd/MM/yyyy"))
                .Replace("{{TotalAmount}}", orderCreatedEvent.TotalAmount.ToString("C"));

            await _emailSender.SendEmailAsync(user.Email, EmailMessages.OrderCreated, templateBody, "html");

            return new Result(true)
                .AddMessage(SuccessMessages.EmailSent(orderCreatedEvent.OrderId, orderCreatedEvent.OrderId));
        }
        catch (Exception ex)
        {
            return new Result(false)
                .AddErrorMessage(ErrorMessages.OrderException(orderCreatedEvent.OrderId, ex.Message))
                .AddErrorMessage(ex.StackTrace ?? "No stack trace available");
        }
    }
}