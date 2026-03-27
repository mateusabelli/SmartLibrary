using LendingService.Dtos;
using LendingService.Models;
using Riok.Mapperly.Abstractions;

namespace LendingService.Mappers;

[Mapper]
public partial class LendMapper
{
    [MapperIgnoreSource(nameof(Lend.Id))]
    [MapperIgnoreSource(nameof(Lend.BorrowedAt))]
    public partial LendCreateDto MapToCreateDto(Lend lend);

    [MapperIgnoreSource(nameof(Lend.BorrowedAt))]
    public partial LendReadDto MapToReadDto(Lend lend);

    [MapperIgnoreTarget(nameof(Lend.Id))]
    [MapperIgnoreTarget(nameof(Lend.BorrowedAt))]
    public partial Lend MapFromCreateDto(LendCreateDto createDto);
}