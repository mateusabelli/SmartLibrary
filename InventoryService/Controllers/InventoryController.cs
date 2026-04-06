using InventoryService.Data;
using InventoryService.Dtos;
using InventoryService.Mapper;
using InventoryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class InventoryController(IInventoryRepository repository, IMessageBusClient messageBusClient) : ControllerBase
{
    private readonly BookMapper _mapper = new();

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
            return NotFound($"No book available with id: {bookId}");

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
}