namespace Monolith.Application.Interfaces;

internal interface IMenuService
{
    Task RunAsync(CancellationToken cancellationToken);
}
