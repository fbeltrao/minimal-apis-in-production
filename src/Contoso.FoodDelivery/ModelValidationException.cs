﻿namespace Contoso.FoodDelivery;

public class ModelValidationException : Exception
{
    public ModelValidationException(string? message) : base(message)
    {
    }
}
