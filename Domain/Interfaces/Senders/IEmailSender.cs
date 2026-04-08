namespace Domain.Interfaces.Senders;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string body, string textPart);
}