using Domain.Interfaces.Senders;
using Infrastructure.Config;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Senders;

public class BrevoEmailSender(IOptions<SmtpBrevoConfig> config) : IEmailSender
{
    private readonly IOptions<SmtpBrevoConfig> _config = config;

    public async Task SendEmailAsync(string email, string subject, string body, string textPart)
    {
        var brevoSmtp = _config.Value;
        
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Ecom_Cassandra", brevoSmtp.Email));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = subject;
        message.Body = new TextPart(textPart) { Text = body };

        using var client = new SmtpClient();
        await client.ConnectAsync(brevoSmtp.Host, brevoSmtp.Port,
            SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(brevoSmtp.UserName, brevoSmtp.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}