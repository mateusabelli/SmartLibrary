using System.Text.Json;
using InventoryService.Data;
using InventoryService.Dtos;

namespace InventoryService.Services;

public class EventProcessor(IServiceScopeFactory scopeFactory) : IEventProcessor
{
    public void ProcessEvent(string message)
    {
        using var scope = scopeFactory.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IInventoryRepository>();

        var bookId = int.Parse(message);

        repo.DecrementBookStock(bookId);
        repo.SaveChanges();
    }
}