using Monolith.Application.DTO;

namespace Monolith.Applications.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto?> GetCategoryAsync(int id);
    Task<List<CategoryDto>> GetCategoriesAsync(int offerId);
}
