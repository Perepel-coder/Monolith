using Monolith.Application.DTO;

namespace Monolith.Application.Interfaces;

public interface IOrderProcessorService
{
    // Обработка заказа
    Task<OrderResult> ProcessOrderAsync(int offerId, int regionId);

    // Расчет спецпредложения
    Task<SpecialOfferResult> CalculateSpecialOfferAsync(
        int offerId,
        int regionId,
        decimal currentPrice);
}
