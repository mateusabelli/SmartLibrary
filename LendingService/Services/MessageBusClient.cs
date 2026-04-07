using System.Text;
using System.Text.Json;
using LendingService.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LendingService.Services;

public class MessageBusClient : IDisposable, IMessageBusClient
{
    private readonly ConnectionFactory _factory;
    private IConnection? _connection;
    private IChannel? _channel;

    public MessageBusClient(IConfiguration configuration)
    {
        _factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMqHostname"] ?? "localhost",
            Port = int.Parse(configuration["RabbitMqPort"] ?? "5672"),
            UserName = configuration["RabbitMqUserName"] ?? "guest",
            Password = configuration["RabbitMqPassword"] ?? "guest"
        };
        Console.WriteLine($"--> Message Bus trying to connect to {configuration["RabbitMqHostname"]}");
    }

    public async Task InitializeRabbitMq(CancellationToken cancellationToken)
    {
        try
        {
            _connection = await _factory.CreateConnectionAsync(cancellationToken);
            _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

            await _channel.QueueDeclareAsync(
                queue: "lend",
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: cancellationToken);

            _connection.ConnectionShutdownAsync += RabbitMq_ConnectionShutdown;

            Console.WriteLine("--> Connected to the Message Bus");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not connect to the Message Bus: {ex.Message}");
        }
    }

    public async Task ProduceLendAsync(int bookId)
    {
        if (_channel is not { IsOpen: true })
        {
            Console.WriteLine("--> Message Bus connection not available");
            return;
        }

        var message = JsonSerializer.Serialize(bookId);
        var body = Encoding.UTF8.GetBytes(message);

        await _channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: "lend",
            body: body);

        Console.WriteLine($"--> Message sent: {message}");
    }

    public void Dispose()
    {
        Console.WriteLine("--> Message Bus Disposed");
        if (_channel is { IsOpen: true })
        {
            _connection?.CloseAsync();
            _channel.CloseAsync();
        }
    }

    private Task RabbitMq_ConnectionShutdown(object sender, ShutdownEventArgs @event)
    {
        Console.WriteLine("--> RabbitMQ Connection Shutdown");
        return Task.CompletedTask;
    }
}