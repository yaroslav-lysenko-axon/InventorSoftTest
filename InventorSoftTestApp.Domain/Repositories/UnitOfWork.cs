using InventorSoftTestApp.Domain.Contexts;
using InventorSoftTestApp.Domain.Repositories.Abstractions;

namespace InventorSoftTestApp.Domain.Repositories;

public class UnitOfWork(InventorSoftDbContext databaseContext) : IUnitOfWork
{
    public async Task Commit()
    {
        await databaseContext.SaveChangesAsync();
    }
}