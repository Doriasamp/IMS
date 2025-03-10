using IMS.WebApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModelsValidations;

public class Sell_EnsureEnoughProductsAvailable : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var sellViewModel = validationContext.ObjectInstance as SellViewModel;
        if (sellViewModel is not null && sellViewModel.Product is not null)
        {
            if (sellViewModel.QuantityToSell > sellViewModel.Product.Quantity)
            {
                return new ValidationResult($"There are only {sellViewModel.Product.Quantity} units of {sellViewModel.Product.ProductName} available",
                    new[] { validationContext.MemberName! });
            }
        }
        return ValidationResult.Success;
    }
}
