using LendingService.Models;

namespace LendingService.Data;

public interface ILendingRepository
{
    bool SaveChanges();

    void CreateLend(Lend lend);

    IQueryable<Lend> GetAllLends();

    Lend? GetLendById(int id);
    
    void CloseLend(Lend lend);
}