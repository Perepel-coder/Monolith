using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monolith.Models;

public class Region
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("region_name")]
    [Required]
    public required string RegionName { get; set; }
}
