using MetaExchange.Application.Interfaces;
using MetaExchange.Infrastructure.Configuration;
using MetaExchange.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MetaExchange.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        ExchangeDataOptions exchangeDataOptions = new();
        configuration.Bind(ExchangeDataOptions.c_SectionName, exchangeDataOptions);

        services.AddScoped<IExchangeRepository>(_ =>
            new JsonExchangeRepository(exchangeDataOptions.DirectoryPath));
        return services;
    }
}