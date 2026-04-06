using InventoryService.Dtos;
using InventoryService.Models;
using Riok.Mapperly.Abstractions;

namespace InventoryService.Mapper;

[Mapper]
public partial class BookMapper
{
    public partial BookReadDto MapToReadDto(Book book);

    public partial List<BookReadDto> MapToListReadDtos(IEnumerable<Book> books);

    [MapperIgnoreTarget(nameof(Book.Id))]
    public partial Book MapToBook(BookCreateDto bookCreateDto);
}