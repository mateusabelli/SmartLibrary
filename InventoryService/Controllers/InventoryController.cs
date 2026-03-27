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
    public ActionResult<IEnumerable<ReadBookDto>> GetAllBooks()
    {
        var books = repository.GetAllBooks();
        return Ok(_mapper.MapToEnumerableDto(books));
    }

    [HttpGet("{id:int}", Name = "GetIndividualBook")]
    public ActionResult<ReadBookDto> GetIndividualBook(int id)
    {
        var book = repository.GetBookById(id);

        if (book is null)
            return NotFound();

        return Ok(_mapper.MapToDto(book));
    }

    [HttpPost]
    public async Task<ActionResult<ReadBookDto>> AddNewBook(AddBookDto bookDto)
    {
        var book = _mapper.MapToBook(bookDto);

        repository.AddBook(book);
        repository.SaveChanges();

        await messageBusClient.ProduceBookAsync(bookDto);

        return CreatedAtRoute(nameof(GetIndividualBook), new { id = book.Id }, book);
    }
}