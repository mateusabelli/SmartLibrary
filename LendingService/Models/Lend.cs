using System.ComponentModel.DataAnnotations;

namespace LendingService.Models;

public class Lend
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int BookId { get; set; }

    [Required]
    public required string Borrower { get; set; }

    [Required]
    public DateTime BorrowedAt { get; set; } = DateTime.Now;
}