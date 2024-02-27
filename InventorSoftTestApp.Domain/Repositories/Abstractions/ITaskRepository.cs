using InventorSoftTestApp.Domain.Models.DbEntities;

namespace InventorSoftTestApp.Domain.Repositories.Abstractions;

public interface ITaskRepository : IGenericRepository<TaskModel>
{
    public Task<List<TaskModel>> GetUncompletedTasksOlderThanTwoMinutes();
}