/* 
    This class is a custom validation attribute that ensures the price of a product is greater than the cost of the inventories.
    The class inherits from the ValidationAttribute class and overrides the IsValid method to implement the custom validation logic.
    The IsValid method receives the model object and the validation context as parameters and returns a ValidationResult object.
    The custom validation logic is implemented in the IsValid method by checking if the price of the product is greater than the sum of the costs of all inventories associated with the product.
    If the price is not greater than the sum of the costs, a ValidationResult object with an error message is returned.
*/

using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness.Validations;


/// <summary>
/// Custom validation attribute to ensure the price of a product is greater than the cost of the inventories.
/// </summary>
public class Product_EnsurePriceIsGreaterThanInventoriesCost : ValidationAttribute
{

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var product = validationContext.ObjectInstance as Product;
        if (product is not null)
        {
            if(!ValidatePricing(product))
            {
                return new ValidationResult(
                    errorMessage:$"Price must be greater than the total cost of inventories: {TotalInventoryCost(product)}$ !",
                    memberNames: new List<string>() { validationContext.MemberName! });
            }
        }

        return ValidationResult.Success;
    }


    private double TotalInventoryCost(Product product)
    {
        if (product?.ProductInventories is null)
        {
            return 0;
        }

        return product.ProductInventories.Sum(i => i.Inventory?.Quantity * i.Inventory?.Price ?? 0);
    }


    private bool ValidatePricing(Product product)
    {
        if (product?.ProductInventories is null || product.ProductInventories.Count <= 0) return true;

        return (TotalInventoryCost(product) <= product.Price);

    }

}

