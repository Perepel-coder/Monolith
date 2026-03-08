namespace Monolith.Application.DTO;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsSelected { get; set; }
    public int? OffersCount { get; set; }
}
