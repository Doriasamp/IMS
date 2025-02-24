using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory;


/// <summary>
/// Plugin to get inventories from memory
/// </summary>
public class InventoryRepository : IInventoryRepository
{
    private List<Inventory> _inventories;

    public InventoryRepository()
    {
        _inventories = new List<Inventory>
            {
            new Inventory { InventoryId = 1, InventoryName = "Bike Seat", Quantity = 10, Price = 2},
            new Inventory { InventoryId = 2, InventoryName = "Bike Body", Quantity = 10, Price = 15},
            new Inventory { InventoryId = 3, InventoryName = "Bike Wheels", Quantity = 20, Price = 8},
            new Inventory { InventoryId = 4, InventoryName = "Bike Pedals", Quantity = 20, Price = 1},
            };
    }



    /// <summary>
    /// Get inventories by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return await Task.FromResult(_inventories);
        }

        return _inventories.Where(i => i.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Get inventory by Id parameter
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Inventory?> GetInventoryByIdAsync(int id)
    {
        return await Task.FromResult(_inventories.FirstOrDefault(i => i.InventoryId == id));
    }


    /// <summary>
    /// Add inventory
    /// </summary>
    /// <param name="inventory"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task AddInventoryAsync(Inventory inventory)
    {
        if (_inventories.Any(i => i.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
        {
            return Task.CompletedTask;
        }

        var maxId = _inventories.Max(i => i.InventoryId);   // Create a new inventory id
        inventory.InventoryId = maxId + 1;
        _inventories.Add(inventory);
        return Task.CompletedTask;
    }



    public Task UpdateInventoryAsync(Inventory inventory)
    {
        var itemToUpdate = _inventories.FirstOrDefault(i => i.InventoryId.Equals(inventory.InventoryId));

        if (_inventories.Any(i => (i.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)) && 
                                  !i.InventoryId.Equals(inventory.InventoryId)) )
            
        {
            return Task.CompletedTask;
        }

        if (itemToUpdate is not null)
        {
            itemToUpdate.InventoryName = inventory.InventoryName;
            itemToUpdate.Quantity = inventory.Quantity;
            itemToUpdate.Price = inventory.Price;
        }

        return Task.CompletedTask;
    }

    public Task DeleteInventoryByIdAsync(int id)
    {
        var itemToDelete = _inventories.FirstOrDefault(i => i.InventoryId.Equals(id));
        if (itemToDelete is not null)
        {
            _inventories.Remove(itemToDelete);
        }
        return Task.CompletedTask;
    }
}


