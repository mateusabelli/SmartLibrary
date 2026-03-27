namespace LendingService.Services;

public interface IEventProcessor
{
    void ProcessEvent(string message);
}