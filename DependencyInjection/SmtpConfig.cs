using Domain.Interfaces;
using Domain.Interfaces.Senders;
using Infrastructure.Config;
using Infrastructure.Providers;
using Infrastructure.Senders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection;

public static class SmtpConfig
{
    public static IServiceCollection SetSmtpConfiguration(this IServiceCollection services, IConfiguration config)
    {
        #region  Brevo
        services.Configure<SmtpBrevoConfig>(config.GetSection("ExternalServices:SmtpBrevo"));
        #endregion
        
        services.AddTransient<IEmailTemplateProvider, FileEmailTemplateProvider>();
        services.AddTransient<IEmailSender, BrevoEmailSender>();
        return services;
    }
}