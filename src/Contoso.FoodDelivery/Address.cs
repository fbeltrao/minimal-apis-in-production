namespace Contoso.FoodDelivery;

public class Address
{
    public string AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string ZipCode { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public Address(
        string addressLine1,
        string zipCode,
        string city,
        string state)
    {
        if (string.IsNullOrEmpty(addressLine1))
        {
            throw new ArgumentException($"'{nameof(addressLine1)}' cannot be null or empty.", nameof(addressLine1));
        }

        if (string.IsNullOrEmpty(zipCode))
        {
            throw new ArgumentException($"'{nameof(zipCode)}' cannot be null or empty.", nameof(zipCode));
        }

        if (string.IsNullOrEmpty(city))
        {
            throw new ArgumentException($"'{nameof(city)}' cannot be null or empty.", nameof(city));
        }

        if (string.IsNullOrEmpty(state))
        {
            throw new ArgumentException($"'{nameof(state)}' cannot be null or empty.", nameof(state));
        }

        AddressLine1 = addressLine1;
        ZipCode = zipCode;
        City = city;
        State = state;
    }
}
