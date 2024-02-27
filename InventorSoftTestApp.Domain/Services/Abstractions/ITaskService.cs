using InventorSoftTestApp.Domain.Models.Dtos;

namespace InventorSoftTestApp.Domain.Services.Abstractions;

public interface ITaskService
{
    Task<TaskDto> Create(TaskDto taskDto);
    Task<IReadOnlyCollection<TaskDto>> GetTasks();
}