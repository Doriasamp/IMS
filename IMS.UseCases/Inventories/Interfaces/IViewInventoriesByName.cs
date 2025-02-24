using IMS.CoreBusiness;

namespace IMS.UseCases.Inventories;

public interface IViewInventoriesByName
{
    Task<IEnumerable<Inventory>> ExecuteAsync(string name = "");
}

