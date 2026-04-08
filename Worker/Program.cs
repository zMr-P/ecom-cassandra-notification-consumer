using DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);

var config = builder.Configuration;
builder.Environment.ContentRootPath = Path.GetFullPath("..");

builder.Services
    .SetCassandraConfiguration(config)
    .SetRabbitMqConfig(config)
    .SetSmtpConfiguration(config)
    .SetIocConfig();

var host = builder.Build();
host.Run();