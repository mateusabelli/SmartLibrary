using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Models;

public class Book
{
    [Key]
    public int Id { get; init; }

    [Required]
    [MaxLength(100)]
    public required string Title { get; set; }

    [Required]
    [Column(TypeName = "decimal(6, 2)")]
    public required decimal Cost { get; set; }

    [Required]
    public required int Stock { get; set; }
}