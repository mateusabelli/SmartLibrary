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
    ILendingRepository lendingRepository,
    IDataClient dataClient
) : ControllerBase
{
    private readonly LendMapper _lendMapper = new();

    [HttpGet("{lendId:int}", Name = "GetLendById")]
    public ActionResult<LendReadDto> GetLendById(int lendId)
    {
        var lendFound = lendingRepository.GetAllLends().FirstOrDefault(l => l.Id == lendId);

        if (lendFound is null)
            return NotFound($"Unable to find lend with id: {lendId}");

        return Ok(_lendMapper.MapToReadDto(lendFound));
    }

    [HttpPost]
    public async Task<ActionResult<LendReadDto>> CreateLend(LendCreateDto createDto)
    {
        var newLend = _lendMapper.MapFromCreateDto(createDto);
        var inventoryStock = dataClient.CheckStock(createDto.BookId);

        if (inventoryStock is { Stock: <= 0 })
            return Ok("Out of stock");

        lendingRepository.CreateLend(newLend);
        await messageBusClient.ProduceLendAsync(newLend.BookId);
        lendingRepository.SaveChanges();

        var lendReadDto = _lendMapper.MapToReadDto(newLend);

        return CreatedAtRoute(nameof(GetLendById), new { lendId = newLend.Id }, lendReadDto);
    }
}