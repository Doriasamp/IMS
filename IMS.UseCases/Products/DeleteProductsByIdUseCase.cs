using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products.Interfaces;


namespace IMS.UseCases.Products;
public class DeleteProductsByIdUseCase: IDeleteProductByIdUseCase
{
    private readonly IProductRepository productRepository;

    public DeleteProductsByIdUseCase(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task ExecuteAsync(int productId)
    {
        await productRepository.DeleteProductByIdAsync(productId);
    }
}
