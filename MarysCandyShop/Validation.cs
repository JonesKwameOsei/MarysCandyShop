
using MarysCandyShop.Models;

namespace MarysCandyShop;

public static class Validation
{
    public static CocoaValidationResponse IsCocoaInputValid(string? cocoaInput)
    {
        var response = new CocoaValidationResponse()
        {
            IsValid = true
        };


        if (string.IsNullOrEmpty(cocoaInput))
        {
            response.IsValid = false;
            response.ErrorMessage = "Cocoa percentage is required. Enter a whole number.";
            return response;
        }

        if (!int.TryParse(cocoaInput, out int cocoaPercentage))
        {
            response.IsValid = false;
            response.ErrorMessage = "Not a valid input. Enter a whole number";
            return response;
        }

        if (cocoaPercentage < 0 || cocoaPercentage > 99)
        {
            response.IsValid = false;
            response.ErrorMessage = "Cocoa Percentage must be between 0 and 99.";
            return response;

        }
        response.CocoaPercentage = cocoaPercentage;
        return response;
    }

    public static PriceValidationResponse IsPriceValid(string? priceInput)
    {
        var response = new PriceValidationResponse
        {
            IsValid = true,
        };

        if (string.IsNullOrEmpty(priceInput))
        {
            response.IsValid = false;
            response.ErrorMessage = "Price is required. Please enter a positive number.";
            return response;
        }

        if (!decimal.TryParse(priceInput, out decimal price))
        {
            response.IsValid = false;
            response.ErrorMessage = "Invalid price format. Please enter a valid number";
            return response;
        }

        if (price <= 0 || price > 9999)
        {
            response.IsValid = false;
            response.ErrorMessage = "Price must be greater than 0 and less than or equal to 9999.";
            return response;
        }

        response.Price = price;
        return response;
    }

    public static bool IsStringValid(string name)
    {
        return !string.IsNullOrEmpty(name) &&
           name.Length <= 20 &&
           !name.Any(char.IsDigit);
    }
}
