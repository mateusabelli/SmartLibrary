namespace InventoryService.Dtos;

public class BookReadDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public decimal Cost { get; set; }
    public int Stock { get; set; }
    public bool IsDeleted { get; set; }
}