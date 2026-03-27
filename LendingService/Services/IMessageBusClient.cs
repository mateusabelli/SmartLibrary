using LendingService.Dtos;

namespace LendingService.Services;

public interface IMessageBusClient
{
    Task InitializeRabbitMq(CancellationToken cancellationToken);
    Task ConsumeBookAsync();
    Task ProduceLendAsync(int bookId);
}