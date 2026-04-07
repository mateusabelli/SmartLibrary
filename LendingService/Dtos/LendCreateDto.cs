namespace LendingService.Dtos;

public class LendCreateDto
{
    public required int BookId { get; set; }

    public required string Borrower { get; set; }
}