using LendingService.Data;
using LendingService.Dtos;
using LendingService.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace LendingService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class BooksController(IBookRepository bookRepository) : ControllerBase
{
    private readonly BookMapper _bookMapper = new();

    [HttpGet]
    public ActionResult<List<BookReadDto>> GetAllBooks()
    {
        var books = bookRepository.GetAllBooks().ToList();
        return Ok(_bookMapper.MapToReadDtos(books));
    }

    [HttpGet("{id:int}")]
    public ActionResult<BookReadDto> GetBookById(int id)
    {
        var book = bookRepository.GetBookById(id);

        if (book is null)
            return NotFound();

        return Ok(_bookMapper.MapToReadDto(book));
    }
}