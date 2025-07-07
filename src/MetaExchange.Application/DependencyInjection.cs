using MetaExchange.Application.Interfaces;
using MetaExchange.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MetaExchange.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMetaExchangeService, MetaExchangeService>();
        return services;
    }
}