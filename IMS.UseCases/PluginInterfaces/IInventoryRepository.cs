using IMS.CoreBusiness;
namespace IMS.UseCases.PluginInterfaces;


public interface IInventoryRepository
{
    Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name);

    Task<Inventory?> GetInventoryByIdAsync(int id);

    Task AddInventoryAsync(Inventory inventory);

    Task UpdateInventoryAsync(Inventory inventory);

    Task DeleteInventoryByIdAsync(int id);

}
