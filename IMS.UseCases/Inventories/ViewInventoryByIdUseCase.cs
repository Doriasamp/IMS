
using IMS.CoreBusiness;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Inventories;
public class ViewProducsByIdUseCase: IViewInventoryByIdUseCase
{
    private readonly IInventoryRepository inventoryRepository;


    /// <summary>
    /// Constructor, where the instance of IInventoryRepository is injected by the DI container.
    /// </summary>
    /// <param name="inventoryRepository"></param>
    public ViewProducsByIdUseCase(IInventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }


    public async Task<Inventory?> ExecuteAsync(int id)
    {
       return await inventoryRepository.GetInventoryByIdAsync(id);
    }

}

