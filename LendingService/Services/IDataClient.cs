using InventoryService;

namespace LendingService.Services;

public interface IDataClient
{
    BookStockResponse? CheckStock(int id);
}