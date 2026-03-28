using Grpc.Net.Client;
using InventoryService;

namespace LendingService.Services;

public class DataClient(IConfiguration configuration) : IDataClient
{
    public BookStockResponse? CheckStock(int id)
    {
        var grpcAddress = configuration["GrpcInventory"];
        ArgumentException.ThrowIfNullOrEmpty(grpcAddress);

        Console.WriteLine($"--> Calling gRPC server at {grpcAddress}");

        var channel = GrpcChannel.ForAddress(grpcAddress);
        var client = new GrpcInventory.GrpcInventoryClient(channel);

        try
        {
            var reply = client.CheckStock(new BookStockRequest { Id = id });
            return reply;
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Could not call gRPC server: {e.Message}");
            return null;
        }
    }
}