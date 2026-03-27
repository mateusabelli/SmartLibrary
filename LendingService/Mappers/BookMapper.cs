using LendingService.Dtos;
using LendingService.Models;
using Riok.Mapperly.Abstractions;

namespace LendingService.Mappers;

[Mapper]
public partial class BookMapper
{
    public partial BookReadDto? MapToReadDto(Book? book);

    public partial List<BookReadDto> MapToReadDtos(List<Book> books);

    [MapPropertyFromSource(nameof(Book.IsAvailable), Use = nameof(MapIsAvailable))]
    public partial BookReadDto InventoryBookToReadDto(InventoryBook inventoryBook);

    private bool MapIsAvailable(InventoryBook book) => book.Stock > 0;

    public partial Book MapInventoryToBook(BookReadDto bookReadDto);
}