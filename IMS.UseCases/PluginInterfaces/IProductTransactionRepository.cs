using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces;
public interface IProductTransactionRepository
{
    public Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy, double? price);

    public Task SellProductAsync(string salesOrderNumber, Product product, int quantity, double price, string doneBy);
}
