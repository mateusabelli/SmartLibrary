using InventoryService.Models;

namespace InventoryService.Data;

public interface IInventoryRepository
{
    bool SaveChanges();

    IEnumerable<Book> GetAllBooks();

    Book? GetBookById(int id);

    void AddBook(Book book);

    void DecrementBookStock(int bookId);
}