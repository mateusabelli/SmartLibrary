using LendingService.Models;
using Microsoft.EntityFrameworkCore;

namespace LendingService.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Lend> Lends { get; set; }
    public DbSet<Book> Books { get; set; }
}