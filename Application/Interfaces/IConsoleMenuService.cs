namespace Monolith.Applications.Interfaces;

public interface IConsoleMenuService
{
    Task RunAsync(CancellationToken cancellationToken);
}
