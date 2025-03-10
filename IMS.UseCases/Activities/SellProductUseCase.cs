using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Activities.Interfaces;
using IMS.CoreBusiness;

namespace IMS.UseCases.Activities;


public class SellProductUseCase: ISellProductUseCase
{

    private readonly IProductRepository _productRepository; // product repository dependency for entity Product
    private readonly IProductTransactionRepository _productTransactionRepository;   // product transaction repository dependency for entity ProductTransaction


    public SellProductUseCase(IProductRepository productRepository, IProductTransactionRepository productTransactionRepository)
    {
        // Dependency Injection
        _productRepository = productRepository;
        _productTransactionRepository = productTransactionRepository;
    }


    public async Task ExecuteAsync(string salesOrderNumber, Product product, int quantity, double unitPrice, string doneBy)
    {
        await this._productTransactionRepository.SellProductAsync(salesOrderNumber, product, quantity, unitPrice, doneBy);
        product.Quantity -= quantity;
        await this._productRepository.UpdateProductAsync(product);
    }

}
