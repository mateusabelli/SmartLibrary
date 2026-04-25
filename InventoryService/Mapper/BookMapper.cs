using InventoryService.Dtos;
using InventoryService.Models;
using Riok.Mapperly.Abstractions;

namespace InventoryService.Mapper;

[Mapper]
public partial class BookMapper
{
    [MapProperty(nameof(Book.deletedAt), nameof(BookReadDto.IsDeleted), Use = "DeletedAtToBool")]
    public partial BookReadDto MapToReadDto(Book book);

    [NamedMapping("DeletedAtToBool")]
    private bool MapDeletion(DateTime? deletedAt)
    {
        return deletedAt.HasValue;
    }

    public partial List<BookReadDto> MapToListReadDtos(IEnumerable<Book> books);

    [MapperIgnoreTarget(nameof(Book.Id))]
    [MapperIgnoreTarget(nameof(Book.deletedAt))]
    public partial Book MapToBook(BookCreateDto bookCreateDto);
}