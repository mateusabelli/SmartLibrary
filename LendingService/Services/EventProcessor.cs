using System.Text.Json;
using LendingService.Data;
using LendingService.Mappers;
using LendingService.Models;

namespace LendingService.Services;

public class EventProcessor(IServiceScopeFactory scopeFactory) : IEventProcessor
{
    private readonly BookMapper _bookMapper = new();

    public void ProcessEvent(string message)
    {
        using var scope = scopeFactory.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IBookRepository>();

        var inventoryBook = JsonSerializer.Deserialize<InventoryBook>(message);

        if (inventoryBook == null) return;

        var book = _bookMapper.InventoryBookToReadDto(inventoryBook);

        repo.AddBook(_bookMapper.MapInventoryToBook(book));
        repo.SaveChanges();
    }
}