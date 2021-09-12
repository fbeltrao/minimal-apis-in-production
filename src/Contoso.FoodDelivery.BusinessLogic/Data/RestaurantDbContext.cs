using Microsoft.EntityFrameworkCore;

namespace Contoso.FoodDelivery.BusinessLogic.Data;

public sealed class RestaurantDbContext : DbContext
{
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();

    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }
}
