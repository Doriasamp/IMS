using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Inventories;

public class ViewInventoriesByName: IViewInventoriesByName
{

    private readonly IInventoryRepository inventoryRepository;


    /// <summary>
    /// Constructor, where the instance of IInventoryRepository is injected by the DI container.
    /// </summary>
    /// <param name="inventoryRepository"></param>
    public ViewInventoriesByName(IInventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    public async Task<IEnumerable<Inventory>> ExecuteAsync(string name = "")
    {
        return await inventoryRepository.GetInventoriesByNameAsync(name);
    }


}
