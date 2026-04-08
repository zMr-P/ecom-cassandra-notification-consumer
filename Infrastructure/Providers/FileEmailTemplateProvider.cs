using Domain.Interfaces;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Providers;

public class FileEmailTemplateProvider(IHostEnvironment env) : IEmailTemplateProvider
{
    private readonly IHostEnvironment _env = env;

    public string ReadTemplate(string templateName)
    {
        var path = Path.Combine(_env.ContentRootPath, "Infrastructure", "Templates", $"{templateName}.html");
        return File.ReadAllText(path);
    }
}