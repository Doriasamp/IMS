using IMS.CoreBusiness;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Inventories;

public class AddInventoryUseCase: IAddInventoryUseCase
{
    private readonly IInventoryRepository _inventoryRepository;

    /// <summary>
    /// Dependency Injection of IInventoryRepository through constructor
    /// </summary>
    /// <param name="inventoryRepository"></param>
    public AddInventoryUseCase(IInventoryRepository inventoryRepository)
    {
        this._inventoryRepository = inventoryRepository;
    }

    /// <summary>
    /// Execute method to add inventory
    /// </summary>
    /// <param name="inventory"></param>
    /// <returns></returns>
    public async Task ExecuteAsync(Inventory inventory)
    {
        await this._inventoryRepository.AddInventoryAsync(inventory);
    }
}
