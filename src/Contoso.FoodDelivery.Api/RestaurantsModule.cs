
using Contoso.FoodDelivery.BusinessLogic.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Contoso.FoodDelivery.Api;
public static class RestaurantsModule
{
    public static IServiceCollection AddRestaurants(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEntityFrameworkSqlite();
        services.AddDbContext<FoodDeliveryDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("ConnectionString") ?? "Data Source=restaurants.db";
            options.UseSqlite(connectionString, db =>
            {
            });
        });

        return services;
    }

    public static async Task<WebApplication> SetupRestaurantsAsync(this WebApplication app)
    {
        app.Logger.LogInformation("Initializing database");

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<FoodDeliveryDbContext>();
        await db.Database.EnsureCreatedAsync();

        return app;
    }

    public static IEndpointRouteBuilder UseRestaurants(this IEndpointRouteBuilder app)
    {
        app.MapGet("restaurants", GetRestaurantListHandler)
            .WithDisplayName("get_restaurants");

        return app;
    }


    internal static async Task<IResult> GetRestaurantListHandler(FoodDeliveryDbContext db, CancellationToken cancellationToken)
    {
        var list = await db.Restaurants
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return Results.Ok(list);
    }
}
