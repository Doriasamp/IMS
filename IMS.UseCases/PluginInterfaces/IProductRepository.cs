using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
    Task<Product?> GetProductByIdAsync(int id);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductByIdAsync(int id);
    Task ProduceProductAsync(string productionNumber, Product product, int quantity, string doneBy);
}
