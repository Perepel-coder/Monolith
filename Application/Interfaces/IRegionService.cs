using Monolith.Application.DTO;

namespace Monolith.Application.Interfaces;

public interface IRegionService
{
    Task<List<RegionDto>> GetAllRegionsAsync();
    Task<RegionDto?> GetRegionAsync(int id);
    Task<RegionDto?> GetRegionAsync(string name);
}
