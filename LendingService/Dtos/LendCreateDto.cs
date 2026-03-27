namespace LendingService.Dtos;

public class LendCreateDto
{
    public int BookId { get; set; }

    public required string Borrower { get; set; }
}