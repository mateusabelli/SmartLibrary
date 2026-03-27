using InventoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
}