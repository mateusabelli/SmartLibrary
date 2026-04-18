using LendingService.Dtos;
using LendingService.Models;
using Riok.Mapperly.Abstractions;

namespace LendingService.Mappers;

[Mapper]
public partial class LendMapper
{
    [MapperIgnoreSource(nameof(Lend.Id))]
    [MapperIgnoreSource(nameof(Lend.BorrowedAt))]
    [MapperIgnoreSource(nameof(Lend.isClosed))]
    public partial LendCreateDto MapToCreateDto(Lend lend);

    public partial LendReadDto MapToReadDto(Lend lend);

    [MapperIgnoreTarget(nameof(Lend.Id))]
    [MapperIgnoreTarget(nameof(Lend.BorrowedAt))]
    [MapperIgnoreTarget(nameof(Lend.isClosed))]
    public partial Lend MapFromCreateDto(LendCreateDto createDto);

    public partial IEnumerable<LendReadDto> MapToReadDtos(IEnumerable<Lend> lends);
}