using InventoryService.Dtos;
using InventoryService.Models;
using Riok.Mapperly.Abstractions;

namespace InventoryService.Mapper;

[Mapper]
public partial class BookMapper
{
    public partial ReadBookDto MapToDto(Book book);

    public partial IEnumerable<ReadBookDto> MapToEnumerableDto(IEnumerable<Book> books);

    [MapperIgnoreTarget(nameof(Book.Id))]
    public partial Book MapToBook(AddBookDto addBookDto);
}