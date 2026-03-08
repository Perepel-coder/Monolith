using Microsoft.EntityFrameworkCore;
using Monolith.Application.DTO;
using Monolith.Application.Interfaces;
using Monolith.Applications.Interfaces;

namespace Monolith.Application.Services;

public class RegionService : IRegionService
{
    private readonly IApplicationDbContext _context;

    public RegionService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<RegionDto>> GetAllRegionsAsync()
    {
        return await _context.Regions
            .OrderBy(r => r.RegionName)
            .Select(r => new RegionDto
            {
                Id = r.Id,
                Name = r.RegionName
            })
            .ToListAsync();
    }

    public async Task<RegionDto?> GetRegionAsync(int id)
    {
        var region = await _context.Regions
            .FirstOrDefaultAsync(r => r.Id == id);

        if (region == null)
            return null;

        return new RegionDto
        {
            Id = region.Id,
            Name = region.RegionName
        };
    }

    public async Task<RegionDto?> GetRegionAsync(string name)
    {
        var region = await _context.Regions
            .FirstOrDefaultAsync(r => r.RegionName == name);

        if (region == null)
            return null;

        return new RegionDto
        {
            Id = region.Id,
            Name = region.RegionName
        };
    }
}