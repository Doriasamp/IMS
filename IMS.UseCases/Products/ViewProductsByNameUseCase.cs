
using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products.Interfaces;

namespace IMS.UseCases.Products;
public class ViewProductsByNameUseCase: IViewProductsByNameUseCase
{
    private readonly IProductRepository productRepository;


    /// <summary>
    /// Constructor for ViewProductsByIdUseCase for Dependency Injection
    /// </summary>
    /// <param name="productRepository"></param>
    public ViewProductsByNameUseCase(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }


    public async Task<IEnumerable<Product>> ExecuteAsync(string name = "")
    {
       return await productRepository.GetProductsByNameAsync(name);
    }

}

