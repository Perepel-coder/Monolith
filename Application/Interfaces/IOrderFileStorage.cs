using Monolith.Application.DTO;

namespace Monolith.Application.Interfaces;

public interface IOrderFileStorage
{
    Task SaveOrderAsync(OrderDto order);
    Task<List<OrderDto>> GetSavedOrdersAsync();
    Task<OrderDto?> GetOrderAsync(string orderFileName);
}
