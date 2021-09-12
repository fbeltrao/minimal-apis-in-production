namespace Contoso.FoodDelivery;

public class CannotSetMenuItemFeaturedException : ModelValidationException
{
    public CannotSetMenuItemFeaturedException(string message) : base(message)
    {
    }
}
