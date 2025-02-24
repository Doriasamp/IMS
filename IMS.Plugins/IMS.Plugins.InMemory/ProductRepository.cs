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
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await Task.FromResult(_products.FirstOrDefault(i => i.ProductId == id));
    }


    /// <summary>
    /// Add Product
    /// </summary>
    /// <param name="Product"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task AddProductAsync(Product product)
    {
        if (_products.Any(i => i.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
        {
            return Task.CompletedTask;
        }

        var maxId = _products.Max(i => i.ProductId);   // Create a new Product id
        product.ProductId = maxId + 1;
        _products.Add(product);
        return Task.CompletedTask;
    }



    public Task UpdateProductAsync(Product product)
    {
        var itemToUpdate = _products.FirstOrDefault(i => i.ProductId.Equals(product.ProductId));

        if (_products.Any(i => (i.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)) && 
                                  !i.ProductId.Equals(product.ProductId)) )
            
        {
            return Task.CompletedTask;
        }

        if (itemToUpdate is not null)
        {
            itemToUpdate.ProductName = product.ProductName;
            itemToUpdate.Quantity = product.Quantity;
            itemToUpdate.Price = product.Price;
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
}


