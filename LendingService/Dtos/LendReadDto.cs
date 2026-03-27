namespace LendingService.Dtos;

public class LendReadDto
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public required string Borrower { get; set; }
}