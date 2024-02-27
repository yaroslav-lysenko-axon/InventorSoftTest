using InventorSoftTestApp.Domain.Contexts;
using InventorSoftTestApp.Domain.Models.DbEntities;
using InventorSoftTestApp.Domain.Models.Enums;
using InventorSoftTestApp.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InventorSoftTestApp.Domain.Repositories;

public class TaskRepository(InventorSoftDbContext context)
    : GenericRepository<TaskModel>(context.Tasks), ITaskRepository
{
    public async Task<List<TaskModel>> GetUncompletedTasksOlderThanTwoMinutes()
    {
        IQueryable<TaskModel> tasks = context.Tasks
            .AsNoTracking()
            .Where(task => task.CreatedAt < DateTime.UtcNow.AddMinutes(-2)
                        && task.State != TaskState.Completed);

        return await tasks.ToListAsync();
    }
}