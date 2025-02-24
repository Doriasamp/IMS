using IMS.CoreBusiness;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Inventories;
public class EditInventoryUseCase: IEditInventoryUseCase
{
    private readonly IInventoryRepository _inventoryRepository;

    /// <summary>
    /// Dependency Injection of IInventoryRepository through constructor
    /// </summary>
    /// <param name="inventoryRepository"></param>
    public EditInventoryUseCase(IInventoryRepository inventoryRepository)
    {
        this._inventoryRepository = inventoryRepository;
    }

    public async Task ExecuteAsync(Inventory inventory)
    {
        await this._inventoryRepository.UpdateInventoryAsync(inventory);
    }
}
