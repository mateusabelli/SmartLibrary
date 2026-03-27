using InventoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder applicationBuilder, bool isProd)
    {
        var scope = applicationBuilder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        SeedDb(context, isProd);
    }

    private static void SeedDb(AppDbContext context, bool isProd)
    {
        if (isProd)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }
        }

        if (!context.Books.Any())
        {
            Console.WriteLine("--> Seeding data...");
            context.AddRange(
                new Book { Title = "The Last Compiler", Cost = 24.99m, Stock = 2 },
                new Book { Title = "Private Public Methods", Cost = 18.50m, Stock = 8 },
                new Book { Title = "Managing Lifecycles", Cost = 33.33m, Stock = 4 }
            );
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
    }
}