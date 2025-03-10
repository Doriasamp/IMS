
using IMS.CoreBusiness;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Activities;

public class ProduceProductUseCase : IProduceProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly IProductTransactionRepository _productTransactionRepository;

    // Dependency Injection
    public ProduceProductUseCase(IProductRepository productRepository, IProductTransactionRepository productTransactionRepository)
    {
        _productRepository = productRepository;
        _productTransactionRepository = productTransactionRepository;
    }

    public async Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy, double? price= null!)
    {
        // 1. Add a transaction record
        await this._productTransactionRepository.ProduceAsync(productionNumber, product, quantity, doneBy, price);

        // 2. Increase the quantity of the product
        product.Quantity += quantity;
        await this._productRepository.UpdateProductAsync(product);

        // 3. Decrease the quantity of the inventories needed to produce the product

    }

}
