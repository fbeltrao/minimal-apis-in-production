using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Contoso.FoodDelivery.BusinessLogic.Data;

public sealed class FoodDeliveryDbContext : DbContext
{
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();

    public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options)
        : base(options)
    {
    }

    public FoodDeliveryDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            const string connectionString = "Data Source=food_delivery.db";
            optionsBuilder.UseSqlite(connectionString, db =>
            {
            });
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var restaurantEntity = modelBuilder.Entity<Restaurant>();
        restaurantEntity.Property(x => x.Plan)
            .HasConversion(
                    s => s.ToString(),
                    s => Enum.Parse<PlanType>(s));
        restaurantEntity.OwnsOne(x => x.Address);
    }
}

public sealed class FoodDeliveryDesignTimeDbContextFactory : IDesignTimeDbContextFactory<FoodDeliveryDbContext>
{
    public FoodDeliveryDbContext CreateDbContext(string[] args)
    {
        return new FoodDeliveryDbContext();
    }
}
