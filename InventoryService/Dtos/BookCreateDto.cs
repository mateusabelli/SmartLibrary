using System.ComponentModel.DataAnnotations;

namespace InventoryService.Dtos;

public class BookCreateDto
{
    public required string Title { get; set; }
    public required decimal Cost { get; set; }
    public required int Stock { get; set; }
}