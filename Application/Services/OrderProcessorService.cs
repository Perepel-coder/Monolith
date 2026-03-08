using Monolith.Application.DTO;
using Monolith.Application.Interfaces;

namespace Monolith.Application.Services;

public class OrderProcessorService : IOrderProcessorService
{
    private readonly IOfferService _offerService;
    private const int discountPercent = 5;

    public OrderProcessorService(IOfferService offerService) =>
        _offerService = offerService;

    public async Task<OrderResult> ProcessOrderAsync(int offerId, int regionId) =>
        OrderResult.Successful(await _offerService.CreateOrderAsync(offerId, regionId));

    public async Task<SpecialOfferResult> CalculateSpecialOfferAsync(int offerId, int regionId, decimal currentPrice)
    {
        var discountedPrice = currentPrice * (100 - discountPercent) / 100;
        var discountAmount = currentPrice - discountedPrice;

        return new SpecialOfferResult
        {
            IsEligible = true,
            DiscountedPrice = Math.Round(discountedPrice, 2),
            DiscountAmount = Math.Round(discountAmount, 2),
            DiscountPercent = discountPercent,
            Message = $"Вам доступна скидка {discountPercent}% ({discountAmount:N0} руб.)"
        };
    }
}
