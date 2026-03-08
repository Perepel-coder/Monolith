using Microsoft.EntityFrameworkCore;
using Monolith.Models;

namespace Monolith.Applications.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Offer> Offers { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Price> Prices { get; set; }
    DbSet<Region> Regions { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
