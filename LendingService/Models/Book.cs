using System.ComponentModel.DataAnnotations;

namespace LendingService.Models;

public class Book
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public required string Title { get; set; }
    
    [Required]
    public decimal Cost { get; set; }

    [Required]
    public bool IsAvailable { get; set; }
}
