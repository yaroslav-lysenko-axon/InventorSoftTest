namespace InventorSoftTestApp.Domain.Repositories.Abstractions;

public interface IUnitOfWork
{
    public Task Commit();
}