namespace Contoso.FoodDelivery;

public record Address(string AddressLine1, string ZipCode, string City, string State, string? AddressLine2 = null);
