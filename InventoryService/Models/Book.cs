using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Models;

public class Book
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Title { get; set; }

    [Required]
    [Column(TypeName = "decimal(6, 2)")]
    public decimal Cost { get; set; }

    [Required]
    public int Stock { get; set; }
}