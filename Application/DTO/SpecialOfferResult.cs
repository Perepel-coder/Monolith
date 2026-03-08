namespace Monolith.Application.DTO;

public class SpecialOfferResult
{
    public bool IsEligible { get; set; }

    public decimal DiscountedPrice { get; set; }

    public decimal DiscountAmount { get; set; }

    public int DiscountPercent { get; set; } = 5;

    public string Message { get; set; }
}
