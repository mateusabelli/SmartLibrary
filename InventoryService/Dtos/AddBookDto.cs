namespace InventoryService.Dtos;

public class AddBookDto
{
    public required string Title { get; set; }
    public decimal Cost { get; set; }
    public int Stock { get; set; }
}