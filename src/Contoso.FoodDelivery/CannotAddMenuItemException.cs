namespace Contoso.FoodDelivery;

public class CannotAddMenuItemException : ModelValidationException
{
    public CannotAddMenuItemException(string message) : base(message)
    {
    }
}
