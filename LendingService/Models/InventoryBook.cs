using System.ComponentModel.DataAnnotations;

namespace LendingService.Models;

public class InventoryBook
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Title { get; set; }

    [Required]
    public decimal Cost { get; set; }

    [Required]
    public int Stock { get; set; }
}