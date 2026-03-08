namespace Monolith.Application.DTO;

public class OrderDto
{
    public int OfferId { get; set; }
    public string OfferName { get; set; }

    public int RegionId { get; set; }
    public string RegionName { get; set; }

    public decimal Price { get; set; }

    public decimal? OriginalPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal? DiscountAmount =>
        OriginalPrice.HasValue ? OriginalPrice - Price : null;
}
