using InventoryService.Dtos;

namespace InventoryService.Services;

public interface IMessageBusClient
{
    Task InitializeRabbitMq(CancellationToken cancellationToken);
    Task ConsumeLendAsync();
}