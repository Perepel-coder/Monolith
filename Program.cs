using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Monolith;
using Monolith.Application.Interfaces;
using Monolith.Applications.Interfaces;
using Monolith.Applications.Services;
using Monolith.Console;

class Program
{
    static async Task Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        {
            builder.Services.AddSingleton<IApplicationDbContext>(new ApplicationContext());

            builder.Services.AddApplication();

            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddHostedService<OrderConsoleService>();
        }

        using IHost host = builder.Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<Program>>();

            // Проверяем подключение к БД
            var context = services.GetRequiredService<IApplicationDbContext>();
            logger.LogInformation("Подключение к базе данных успешно");

            // Проверяем наличие регионов
            var regionService = services.GetRequiredService<IRegionService>();
            var regions = await regionService.GetAllRegionsAsync();
            logger.LogInformation("Загружено регионов: {Count}", regions.Count);
        }

        await host.RunAsync();
    }
}