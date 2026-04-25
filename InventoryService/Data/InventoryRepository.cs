using InventoryService.Models;

namespace InventoryService.Data;

public class InventoryRepository(AppDbContext context) : IInventoryRepository
{
    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return context.Books.AsEnumerable();
    }

    public Book? GetBookById(int id)
    {
        return context.Books.FirstOrDefault(b => b.Id == id);
    }

    public void AddBook(Book book)
    {
        ArgumentNullException.ThrowIfNull(book);
        context.Books.Add(book);
    }

    public void DecrementBookStock(int bookId)
    {
        var book = context.Books.FirstOrDefault(b => b.Id == bookId);
        ArgumentNullException.ThrowIfNull(book);

        book.Stock -= 1;

        context.Books.Update(book);
    }
}