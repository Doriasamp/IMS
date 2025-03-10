using IMS.CoreBusiness;
using System.ComponentModel.DataAnnotations;
using IMS.WebApp.ViewModelsValidations;  // Required to use the custom validation attribute defined in the IMS.WebApp.ViewModelsValidations namespace

namespace IMS.WebApp.ViewModels;
public class ProduceViewModel
{
    [Required]
    public string ProductionNumber { get; set; } = string.Empty;

    [Range(minimum:1, maximum:int.MaxValue, ErrorMessage = "You have to select a product.")]
    public int ProductId { get; set; }

    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantity has to be greater than 0.")]
    [Produce_EnsureEnoughInventoryQuantity]
    public int QuantityToProduce { get; set; }  // Property with a custom validation attribute defined in the IMS.WebApp.ViewModelsValidations namespace

    // Navigation property for the Product entity and for custom validation
    public Product? Product { get; set; } = null;
}

