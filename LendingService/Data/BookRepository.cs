using LendingService.Models;

namespace LendingService.Data;

public class BookRepository(AppDbContext context) : IBookRepository
{
    public bool SaveChanges()
    {
        return context.SaveChanges() >= 0;
    }

    public IQueryable<Book> GetAllBooks()
    {
        return context.Books.AsQueryable();
    }

    public Book? GetBookById(int id)
    {
        return context.Books.FirstOrDefault(b => b.Id == id);
    }

    public void AddBook(Book book)
    {
        context.Books.Add(book);
    }
}