using Microsoft.Extensions.Logging;
using Monolith.Application.DTO;
using Monolith.Application.Interfaces;

namespace Monolith.Application.Services;

public class OrderConfirmationService : IOrderConfirmationService
{
    private readonly IOfferService _offerService;
    private readonly IOrderFileStorage _fileStorage;

    public OrderConfirmationService(
        IOfferService offerService,
        IOrderFileStorage fileStorage,
        ILogger<OrderConfirmationService> logger)
    {
        _offerService = offerService;
        _fileStorage = fileStorage;
    }

    public async Task<bool> ConfirmOrderAsync(OrderDto order)
    {
        try
        {
            await _fileStorage.SaveOrderAsync(order);
            return true;
        }
        catch
        {
            return false;
        }
    }
}