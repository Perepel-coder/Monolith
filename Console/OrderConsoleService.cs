using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Monolith.Application.Interfaces;

namespace Monolith.Console;

public class OrderConsoleService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public OrderConsoleService(IServiceScopeFactory scopeFactory) =>
         _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var menuService = scope.ServiceProvider.GetRequiredService<IMenuService>();
                    await menuService.RunAsync(stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                UIHelper.ShowMessage(
                    "Произошла критическая ошибка. Перезапуск через 5 секунд...",
                    MessageType.Error);

                await Task.Delay(5000, stoppingToken);
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        UIHelper.ShowMessage("Приложение завершает работу...", MessageType.Info);
        await base.StopAsync(cancellationToken);
    }
}