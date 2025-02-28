
using System.ComponentModel.DataAnnotations;
using IMS.CoreBusiness.Validations;

namespace IMS.CoreBusiness;
public class Product
{
    public int ProductId { get; set; }

    [Required] [StringLength(140)]
    public string ProductName { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 0.")]
    public int Quantity { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Price must be greater than or equal to 0.")]
    public double Price { get; set; }

    [Product_EnsurePriceIsGreaterThanInventoriesCost]
    public List<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();  // Navigation property that uses a custom validation attribute defined in the IMS.CoreBusiness.Validations namespace



    public void AddInventory(Inventory inventory)
    {
        if (!this.ProductInventories.Any(pi => pi.Inventory is not null && pi.Inventory.InventoryName.Equals(inventory.InventoryName)))
        {
            this.ProductInventories.Add(new ProductInventory
            {
                InventoryId = inventory.InventoryId,
                Inventory = inventory,
                InventoryQuantity = 1,
                ProductId = this.ProductId,
                Product = this
            });
        }
    }


    public void RemoveInventory(ProductInventory prodInventory)
    {
        this.ProductInventories?.Remove(prodInventory);
    }


}

