using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness;

public class Inventory
{
    public int InventoryId { get; set; }
    [Required]
    [StringLength(maximumLength: 50, ErrorMessage = "Item name must be less than 50 characters")]
    public string InventoryName { get; set; } = string.Empty;
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 0")]
    public int Quantity { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Price be greater or equal to 0")]
    public double Price { get; set; }
}

