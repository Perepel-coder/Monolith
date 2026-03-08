using Microsoft.EntityFrameworkCore;
using Monolith.Applications.Interfaces;
using Monolith.Models;

namespace Monolith;

public partial class ApplicationContext : DbContext, IApplicationDbContext
{
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<Region> Regions { get; set; }

    public ApplicationContext()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyDB.db");

        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Offer>()
            .HasMany(o => o.Categories)
            .WithMany(c => c.Offers)
            .UsingEntity<Dictionary<string, object>>(
                "offer_categories",
                j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                j => j.HasOne<Offer>().WithMany().HasForeignKey("OfferId"),
                j =>
                {
                    j.ToTable("offer_categories");
                    j.HasKey("OfferId", "CategoryId");
                });

        modelBuilder.Entity<Price>()
            .HasOne(p => p.Offer)
            .WithMany(o => o.Prices)
            .HasForeignKey(p => p.OfferId)
            .OnDelete(DeleteBehavior.Cascade);

        SetData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}