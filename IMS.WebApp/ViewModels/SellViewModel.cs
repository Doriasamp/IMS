using IMS.CoreBusiness;
using System.ComponentModel.DataAnnotations;
using IMS.WebApp.ViewModelsValidations;  // Required to use the custom validation attribute defined in the IMS.WebApp.ViewModelsValidations namespace

namespace IMS.WebApp.ViewModels;


 public class SellViewModel
 {
    [Required]
    public string SalesOrderNumber{ get; set; } = string.Empty;
    [Range(minimum:1, maximum:int.MaxValue, ErrorMessage ="You have to select a product.")]
    public int ProductId { get; set; }
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantity has to be greater or equal to 1.")]
    [Sell_EnsureEnoughProductsAvailable]
    public int QuantityToSell { get; set; }
    [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "Price cannot be negative.")]
    public double UnitPrice { get; set; }
    // Navigation property for the Product entity and for custom validation
    public Product? Product { get; set; } = null;
}
