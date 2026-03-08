using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monolith.Models;

public class Offer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [Required]
    public required string Name { get; set; } = string.Empty;

    public List<Category> Categories { get; set; } = new();

    public List<Price> Prices { get; set; } = new();
}
