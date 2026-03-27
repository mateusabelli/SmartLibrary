using LendingService.Models;

namespace LendingService.Data;

public interface IBookRepository
{
    IQueryable<Book> GetAllBooks();

    Book? GetBookById(int id);

    void AddBook(Book book);

    bool SaveChanges();
}