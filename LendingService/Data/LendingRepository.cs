using LendingService.Models;

namespace LendingService.Data;

public class LendingRepository(AppDbContext context) : ILendingRepository
{
    public bool SaveChanges()
    {
        return context.SaveChanges() >= 0;
    }

    public void CreateLend(Lend lend)
    {
        ArgumentNullException.ThrowIfNull(lend);
        context.Lends.Add(lend);
    }

    public IQueryable<Lend> GetAllLends()
    {
        return context.Lends;
    }

    public Lend? GetLendById(int id)
    {
        return context.Lends.FirstOrDefault(lend => lend.Id == id);
    }

    public void CloseLend(Lend lend)
    {
        ArgumentNullException.ThrowIfNull(lend);
        context.Lends.Update(lend);
        lend.isClosed = true;
    }
}