using InventoryService.Data;
using InventoryService.Dtos;
using InventoryService.Mapper;
using InventoryService.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class InventoryController(IInventoryRepository repository) : ControllerBase
{
    private readonly BookMapper _mapper = new();
    private const string NotFoundMessage = "The requested book with id: {0} does not exist or was deleted";

    [HttpGet]
    public ActionResult<List<BookReadDto>> GetAllBooks()
    {
        var books = repository.GetAllBooks().ToList();

        if (books.Count == 0)
            return NotFound("No data available");

        return Ok(_mapper.MapToListReadDtos(books));
    }

    [HttpGet("{bookId:int}", Name = "GetIndividualBook")]
    public ActionResult<BookReadDto> GetIndividualBook(int bookId)
    {
        var book = repository.GetBookById(bookId);

        if (book is null)
            return NotFound(string.Format(NotFoundMessage, bookId));

        return Ok(_mapper.MapToReadDto(book));
    }

    [HttpPost]
    public ActionResult<BookReadDto> AddNewBook(BookCreateDto bookCreateDto)
    {
        var book = _mapper.MapToBook(bookCreateDto);

        repository.AddBook(book);
        repository.SaveChanges();

        return CreatedAtRoute(nameof(GetIndividualBook), new { bookId = book.Id }, book);
    }

    [HttpPut("{bookId:int}")]
    public ActionResult<BookReadDto> UpdateBook(BookCreateDto bookCreateDto, int bookId)
    {
        var book = repository.GetBookById(bookId);

        if (book is null)
            return NotFound(string.Format(NotFoundMessage, bookId));

        book.Title = bookCreateDto.Title;
        book.Cost = bookCreateDto.Cost;
        book.Stock = bookCreateDto.Stock;

        repository.SaveChanges();

        return Ok(_mapper.MapToReadDto(book));
    }

    [HttpDelete("{bookId:int}")]
    public ActionResult DeleteBook(int bookId)
    {
        var book = repository.GetBookById(bookId);

        if (book is null || book.deletedAt.HasValue)
            return NotFound(string.Format(NotFoundMessage, bookId));

        book.deletedAt = DateTime.UtcNow;

        repository.SaveChanges();
        return NoContent();
    }
}