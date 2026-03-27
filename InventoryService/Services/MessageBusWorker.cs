namespace InventoryService.Services;

public class MessageBusWorker(IMessageBusClient messageBusClient) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await messageBusClient.InitializeRabbitMq(stoppingToken);
        await messageBusClient.ConsumeLendAsync();
    }
}