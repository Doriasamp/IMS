// Association class between Product and Inventory for many-to-many relationship

using System.Text.Json.Serialization;

namespace IMS.CoreBusiness;
public class ProductInventory
{
    public int ProductId { get; set; }
    [JsonIgnore]
    public Product? Product { get; set; }   // Navigation property
    public int InventoryId { get; set; }
    [JsonIgnore]
    public Inventory? Inventory { get; set; } // Navigation property
    public int InventoryQuantity { get; set; }

}
