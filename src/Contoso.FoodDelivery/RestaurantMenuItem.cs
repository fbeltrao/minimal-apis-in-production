namespace Contoso.FoodDelivery;

public class RestaurantMenuItem
{
#pragma warning disable CS8618
    /// <summary>
    /// Constructor for EF Core, as it cannot pass a Restaurant in the constructor.
    /// </summary>
    private RestaurantMenuItem() { }
#pragma warning restore CS8618

    internal RestaurantMenuItem(Restaurant restaurant, string name, string description)
    {
        Restaurant = restaurant;
        Name = name;
        Description = description;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool Featured { get; private set; }

    public Restaurant Restaurant { get; }

    public void SetFeatured()
    {
        if (Restaurant.Plan == PlanType.Unlimited)
        {
            Featured = true;
            return;
        }

        throw new CannotSetMenuItemFeaturedException($"Setting featured menu items is only available in {PlanType.Unlimited} plan");
    }
}
