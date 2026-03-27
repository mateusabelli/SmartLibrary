namespace LendingService.Dtos;

public class BookReadDto
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public decimal Cost { get; set; }

    public bool IsAvailable { get; set; }
}