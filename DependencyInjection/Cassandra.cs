using Cassandra;
using Cassandra.Mapping;
using Infrastructure.Config;
using Infrastructure.Mapping;
using Infrastructure.Session;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection;

public static class Cassandra
{
    public static IServiceCollection SetCassandraConfiguration(this IServiceCollection services,
        IConfiguration config)
    {
        var cassandraSection = config.GetSection("ExternalServices:Cassandra");
        services.Configure<CassandraConfig>(cassandraSection);

        services.AddSingleton<CassandraSession>();
        services.AddSingleton<ISession>(sp =>
        {
            var cassandraSession = sp.GetRequiredService<CassandraSession>();
            return cassandraSession.GetSession();
        });

        services.AddSingleton<IMapper>(sp =>
        {
            var session = sp.GetRequiredService<ISession>();
            var mappingConfig = CassandraMapping.GetMappings();
            CassandraMappingUdts.RegisterUdts(session);
            return new Mapper(session, mappingConfig);
        });

        return services;
    }
}