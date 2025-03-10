using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory;


/// <summary>
/// Plugin to get Product from memory
/// </summary>
public class ProductRepository : IProductRepository
{
    private List<Product> _products;

    public ProductRepository()
    {
        _products = new List<Product>
            {
            new Product { ProductId = 1, ProductName = "Bike", Quantity = 10, Price = 150},
            new Product { ProductId = 2, ProductName = "Car", Quantity = 10, Price = 25000},
            };
    }



    /// <summary>
    /// Get products by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return await Task.FromResult(_products);
        }

        return _products.Where(i => i.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Get Product by Id parameter
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        var prod = _products.FirstOrDefault(x => x.ProductId == productId);
        var newProd = new Product();
        if (prod != null)
        {
            newProd.ProductId = prod.ProductId;
            newProd.ProductName = prod.ProductName;
            newProd.Price = prod.Price;
            newProd.Quantity = prod.Quantity;
            newProd.ProductInventories = new List<ProductInventory>();
            if (prod.ProductInventories != null && prod.ProductInventories.Count > 0)
            {
                foreach (var prodInv in prod.ProductInventories)
                {
                    var newProdInv = new ProductInventory
                    {
                        InventoryId = prodInv.InventoryId,
                        ProductId = prodInv.ProductId,
                        Product = prod,
                        Inventory = new Inventory(),
                        InventoryQuantity = prodInv.InventoryQuantity
                    };
                    if (prodInv.Inventory != null)
                    {
                        newProdInv.Inventory.InventoryId = prodInv.Inventory.InventoryId;
                        newProdInv.Inventory.InventoryName = prodInv.Inventory.InventoryName;
                        newProdInv.Inventory.Price = prodInv.Inventory.Price;
                        newProdInv.Inventory.Quantity = prodInv.Inventory.Quantity;
                    }

                    newProd.ProductInventories.Add(newProdInv);
                }
            }
        }

        return await Task.FromResult(newProd);
    }


    /// <summary>
    /// Add Product
    /// </summary>
    /// <param name="Product"></param>
    /// <returns></returns>
    public Task AddProductAsync(Product product)
    {
        if (_products.Any(i => i.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
        {
            return Task.CompletedTask;  // Product already exists
        }

        var maxId = _products.Max(i => i.ProductId);   // Create a new Product id
        product.ProductId = maxId + 1;
        _products.Add(product);
        return Task.CompletedTask;
    }



    public Task UpdateProductAsync(Product product)
    {
        if (_products.Any(x => x.ProductId != product.ProductId &&
                               x.ProductName.ToLower() == product.ProductName.ToLower()))
            return Task.CompletedTask;

        var prod = _products.FirstOrDefault(x => x.ProductId == product.ProductId);
        if (prod is not null)
        {
            prod.ProductName = product.ProductName;
            prod.Quantity = product.Quantity;
            prod.Price = product.Price;
            prod.ProductInventories = product.ProductInventories;
        }

        return Task.CompletedTask;
    }


    public Task DeleteProductByIdAsync(int id)
    {
        var itemToDelete = _products.FirstOrDefault(i => i.ProductId.Equals(id));
        if (itemToDelete is not null)
        {
            _products.Remove(itemToDelete);
        }
        return Task.CompletedTask;
    }


    public Task ProduceProductAsync(string productionNumber, Product product, int quantity, string doneBy)
    {
        throw new NotImplementedException();
    }
}


