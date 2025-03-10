
using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory;
public class ProductTransactionRepository : IProductTransactionRepository
{
    private readonly IProductRepository _productRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryTransactionRepository _inventoryTransactionRepository;
    private List<ProductTransaction> _productTransactions = new List<ProductTransaction>();



    // Constructor for dependency injection of the in-memory repositories
    public ProductTransactionRepository(IProductRepository productRepository,
        IInventoryTransactionRepository inventoryTransactionRepository,
        IInventoryRepository inventoryRepository)
    {
        _productRepository = productRepository;
        _inventoryTransactionRepository = inventoryTransactionRepository;
        _inventoryRepository = inventoryRepository;
    }


    public async Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy, double? price)
    {
        var prod = await this._productRepository.GetProductByIdAsync(product.ProductId);
        if (prod is not null)
        {
            foreach (var pi in prod.ProductInventories)
            {
                if (pi.Inventory is not null)
                {
                    // 1. Add inventory transaction
                    await this._inventoryTransactionRepository.ProduceAsync(productionNumber,
                        pi.Inventory,
                        pi.InventoryQuantity * quantity,
                        doneBy,
                        -1);

                    // 2. Decrease the inventory
                    var inv = await this._inventoryRepository.GetInventoryByIdAsync(pi.Inventory.InventoryId);
                    if (inv is not null)
                    {
                        inv.Quantity -= pi.InventoryQuantity * quantity;
                        await this._inventoryRepository.UpdateInventoryAsync(inv);
                    }
                }
            }
        }


        // 3. add the product transaction
        this._productTransactions.Add(new ProductTransaction
        {
            ProductionNumber = productionNumber,
            ProductId = product.ProductId,
            QuantityBefore = product.Quantity,
            ActivityType = ProductTransactionType.ProduceProduct,
            QuantityAfter = product.Quantity + quantity,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy
        });
    }


    public Task SellProductAsync(string salesOrderNumber, Product product, int quantity, double price, string doneBy)
    {
        this._productTransactions.Add(new ProductTransaction
        {
            ActivityType = ProductTransactionType.SellProduct,
            SONumber = salesOrderNumber,
            ProductId = product.ProductId,
            QuantityBefore = product.Quantity,
            QuantityAfter = product.Quantity - quantity,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
            UnitPrice = price
        });

        return Task.CompletedTask;
    }

}

