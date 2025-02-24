using IMS.CoreBusiness;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Inventories;

public class DeleteInventoryUseCase: IDeleteInventoryUseCase
{
    private readonly IInventoryRepository _inventory;

    /// <summary>
    /// Constructor for Dependency Injection
    /// </summary>
    /// <param name="inventoryRepository"></param>
    public DeleteInventoryUseCase(IInventoryRepository inventoryRepository)
    {
        this._inventory = inventoryRepository;
    }


    public async Task ExecuteAsync(int inventoryId)
    {
        await this._inventory.DeleteInventoryByIdAsync(inventoryId);
    }
}

