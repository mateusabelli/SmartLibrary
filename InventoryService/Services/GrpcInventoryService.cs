using Grpc.Core;
using InventoryService.Data;

namespace InventoryService.Services;

public class GrpcInventoryService(IInventoryRepository repository) : GrpcInventory.GrpcInventoryBase
{
    public override Task<BookStockResponse> CheckStock(BookStockRequest request, ServerCallContext context)
    {
        if (request.Id <= 0)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Id must be greater or igual to 0"));

        var book = repository.GetBookById(request.Id);

        if (book == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Could not find book with id {request.Id}"));

        return Task.FromResult(new BookStockResponse
        {
            Id = book.Id,
            Stock = book.Stock
        });
    }
}