namespace Monolith.Application.DTO;

public class OfferDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public decimal Price { get; set; }

    public int RegionId { get; set; }

    public string RegionName { get; set; }

    public List<string> Categories { get; set; } = new();

    public decimal? DiscountedPrice { get; set; }

    public bool HasDiscount => DiscountedPrice.HasValue;
}
