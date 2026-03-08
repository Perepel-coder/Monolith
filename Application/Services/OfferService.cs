using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Monolith.Application.DTO;
using Monolith.Application.Interfaces;
using Monolith.Applications.Interfaces;

namespace Monolith.Application.Services
{
    public class OfferService : IOfferService
    {
        private readonly IApplicationDbContext _context;

        public OfferService(IApplicationDbContext context, ILogger<OfferService> logger) => _context = context;

        public async Task<List<OfferDto>> GetOffers(int regionId, List<int> categoryIds)
        {
            var query = _context.Offers
                .Include(o => o.Prices)
                    .ThenInclude(p => p.Region)
                .Include(o => o.Categories)
                .Where(o => o.Prices.Any(p => p.RegionId == regionId));

            // Фильтр по категориям (если выбраны)
            if (categoryIds != null && categoryIds.Any())
                query = query.Where(o => o.Categories.Any(c => categoryIds.Contains(c.Id)));

            var offers = await query
                .Select(o => new OfferDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Price = o.Prices.First(p => p.RegionId == regionId).Value,
                    RegionId = regionId,
                    RegionName = o.Prices.First(p => p.RegionId == regionId).Region.RegionName,
                    Categories = o.Categories.Select(c => c.Name).ToList()
                })
                .OrderBy(o => o.Name)
                .ToListAsync();

            return offers;
        }

        public async Task<OfferDto?> GetOffer(int offerId, int regionId) =>
            await _context.Offers
                .Include(o => o.Prices)
                    .ThenInclude(p => p.Region)
                .Include(o => o.Categories)
                .Where(o => o.Id == offerId && o.Prices.Any(p => p.RegionId == regionId))
                .Select(o => new OfferDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Price = o.Prices.First(p => p.RegionId == regionId).Value,
                    RegionId = regionId,
                    RegionName = o.Prices!.First(p => p.RegionId == regionId)!.Region!.RegionName,
                    Categories = o.Categories.Select(c => c.Name).ToList()
                })
            .SingleOrDefaultAsync();

        public async Task<OfferDto?> GetCheapestOfferInCategoryAsync(int categoryId, int regionId, int excludeOfferId = 0)
        {
            var query = _context.Offers
                .Include(o => o.Prices)
                .Include(o => o.Categories)
                .Where(o => o.Categories.Any(c => c.Id == categoryId))
                .Where(o => o.Prices.Any(p => p.RegionId == regionId));

            if (excludeOfferId > 0)
                query = query.Where(o => o.Id != excludeOfferId);

            return await query
                .OrderBy(o => o.Prices.First(p => p.RegionId == regionId).Value)
                .Select(o => new OfferDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Price = o.Prices.First(p => p.RegionId == regionId).Value,
                    RegionId = regionId,
                    RegionName = o.Prices.First(p => p.RegionId == regionId).Region.RegionName,
                    Categories = o.Categories.Select(c => c.Name).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<OrderDto> CreateOrderAsync(int offerId, int regionId, decimal? finalPrice = null)
        {
            var offer = await _context.Offers
                .Include(o => o.Prices)
                    .ThenInclude(p => p.Region)
                .FirstOrDefaultAsync(o => o.Id == offerId);

            if (offer == null)
                throw new InvalidOperationException($"Товар с ID {offerId} не найден");

            var price = offer.Prices.FirstOrDefault(p => p.RegionId == regionId);
            if (price == null)
                throw new InvalidOperationException($"Цена для товара {offerId} в регионе {regionId} не найдена");

            var order = new OrderDto
            {
                OfferId = offer.Id,
                OfferName = offer.Name,
                RegionId = regionId,
                RegionName = price.Region!.RegionName,
                Price = finalPrice ?? price.Value,
                OriginalPrice = finalPrice.HasValue ? price.Value : null,
                CreatedAt = DateTime.Now
            };

            return order;
        }
    }
}