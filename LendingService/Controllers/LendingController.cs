using LendingService.Data;
using LendingService.Dtos;
using LendingService.Mappers;
using LendingService.Services;
using Microsoft.AspNetCore.Mvc;

namespace LendingService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class LendingController(
    IMessageBusClient messageBusClient,
    ILendingRepository lendingRepository) : ControllerBase
{
    private readonly LendMapper _lendMapper = new();

    [HttpGet("{id:int}", Name = "GetLendById")]
    public ActionResult<LendReadDto> GetLendById(int id)
    {
        var lend = lendingRepository.GetAllLends().FirstOrDefault(l => l.Id == id);

        if (lend is null)
            return NotFound();

        return Ok(_lendMapper.MapToReadDto(lend));
    }

    [HttpPost]
    public async Task<ActionResult<LendReadDto>> CreateLend(LendCreateDto createDto)
    {
        var lend = _lendMapper.MapFromCreateDto(createDto);

        lendingRepository.CreateLend(lend);
        lendingRepository.SaveChanges();

        var dto = _lendMapper.MapToReadDto(lend);
        await messageBusClient.ProduceLendAsync(dto.BookId);

        return CreatedAtRoute(nameof(GetLendById), new { id = dto.Id }, dto);
    }
}