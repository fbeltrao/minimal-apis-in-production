namespace Contoso.FoodDelivery;

public enum PlanType
{
    /// <summary>
    /// Restaurants in the basic model cannot contain items.
    /// </summary>
    Basic,

    /// <summary>
    /// Restaurants in this plan can have up to 3 menu items.
    /// Menu items cannot be featured.
    /// </summary>
    Professional,

    /// <summary>
    /// Restaurant in this plan have no limits.
    /// </summary>
    Unlimited,
}
