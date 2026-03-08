using Microsoft.Extensions.DependencyInjection;
using Monolith.Application.Interfaces;
using Monolith.Application.Services;
using Monolith.Applications.Interfaces;
using Monolith.Infrastructure.Services;

namespace Monolith.Applications.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOfferService, OfferService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IRegionService, RegionService>();
        services.AddScoped<IOrderProcessorService, OrderProcessorService>();
        services.AddScoped<IOrderConfirmationService, OrderConfirmationService>();
        services.AddScoped<IOrderFileStorage, OrderFileStorage>();

        return services;
    }
}