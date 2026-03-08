using Monolith.Application.DTO;

namespace Monolith.Application.Interfaces;

public interface IOrderConfirmationService
{
    Task<bool> ConfirmOrderAsync(OrderDto order);
}
