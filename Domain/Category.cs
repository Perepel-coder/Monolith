using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Monolith.Models;

public class Category
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [Required]
    public required string Name { get; set; } = string.Empty;

    [AllowNull]
    public List<Offer> Offers { get; set; }
}
