using InventoryService.Models;

namespace InventoryService.Data;

public interface IInventoryRepository
{
    void SaveChanges();

    IEnumerable<Book> GetAllBooks();

    Book? GetBookById(int id);

    void AddBook(Book book);

    void UpdateBook(Book book);

    void DeleteBook(Book book);

    void DecrementBookStock(int bookId);
}