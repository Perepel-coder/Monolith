using Monolith.Application.DTO;

namespace Monolith.Application.Interfaces;

public interface IOfferService
{
    Task<List<OfferDto>> GetOffers(int regionId, List<int> categoryIds);

    Task<OfferDto?> GetOffer(int offerId, int regionId);

    Task<OfferDto?> GetCheapestOfferInCategoryAsync(int categoryId, int regionId, int excludeOfferId = 0);

    Task<OrderDto> CreateOrderAsync(int offerId, int regionId, decimal? finalPrice = null);
}