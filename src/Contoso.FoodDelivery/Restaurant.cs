namespace Contoso.FoodDelivery;

public class Restaurant
{
    public const int MaxMenuItemsInProfessionalPlan = 10;

    private readonly List<RestaurantMenuItem> _menuItems = new();

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    public PlanType Plan { get; }

    public IReadOnlyList<RestaurantMenuItem> MenuItems => _menuItems;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Restaurant()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Restaurant(string name, PlanType plan, Address address)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
        }

        Name = name;
        Plan = plan;
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }

    public RestaurantMenuItem AddMenuItem(
        string name,
        string description)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
        }

        if (string.IsNullOrEmpty(description))
        {
            throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));
        }

        EnsureCanAddMenuItems();

        var item = new RestaurantMenuItem(this, name, description);
        _menuItems.Add(item);

        return item;
    }

    private void EnsureCanAddMenuItems()
    {
        // DISCLAIMER: this is simple implementation.
        // It does not handle parallel execution of adding menu items to a professional plan.
        var error = (Plan, MenuItems.Count) switch
        {
            (PlanType.Basic, _) => $"In basic plan menu items are not supported. Upgrade to {PlanType.Professional} to additional features",
            (PlanType.Professional, MaxMenuItemsInProfessionalPlan) => $"Limit of {MaxMenuItemsInProfessionalPlan} menu items reached. Upgrade to {PlanType.Unlimited} to unlimited features",
            _ => null,
        };

        if (error != null)
        {
            throw new CannotAddMenuItemException(error);
        }
    }
}
