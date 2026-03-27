namespace InventoryService.Services;

public interface IEventProcessor
{
    void ProcessEvent(string message);
}