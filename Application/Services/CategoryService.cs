using Monolith.Application.DTO;
using Monolith.Applications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Monolith.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IApplicationDbContext _context;

    public CategoryService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _context.Categories
            .OrderBy(c => c.Name)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                IsSelected = false,
                OffersCount = c.Offers.Count
            })
            .ToListAsync();

        return categories;
    }

    public async Task<CategoryDto?> GetCategoryAsync(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Offers)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
            return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            OffersCount = category.Offers.Count
        };
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync(int offerId)
    {
        return await _context.Offers
            .Where(o => o.Id == offerId)
            .SelectMany(o => o.Categories)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }
}