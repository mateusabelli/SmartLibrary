using LendingService.Models;

namespace LendingService.Data;

public interface ILendingRepository
{
    bool SaveChanges();

    void CreateLend(Lend lend);

    IQueryable<Lend> GetAllLends();
}