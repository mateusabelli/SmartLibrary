using System.ComponentModel.DataAnnotations;

namespace LendingService.Models;

public class Lend
{
    [Key]
    public int Id { get; init; }

    [Required]
    public required int BookId { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Borrower { get; set; }

    public DateTime BorrowedAt { get; init; } = DateTime.Now;

    public bool isClosed { get; set; } = false;
}