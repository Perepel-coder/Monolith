using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monolith.Models;

public class Price
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("value")]
    public decimal Value { get; set; }

    [Column("offer_id")]
    public int OfferId { get; set; }

    [Column("region_id")]
    public int RegionId { get; set; }

    public Region? Region { get; set; }

    public Offer? Offer { get; set; }
}
