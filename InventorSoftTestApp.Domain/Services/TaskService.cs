using AutoMapper;
using InventorSoftTestApp.Domain.Models.DbEntities;
using InventorSoftTestApp.Domain.Models.Dtos;
using InventorSoftTestApp.Domain.Models.Enums;
using InventorSoftTestApp.Domain.Repositories.Abstractions;
using InventorSoftTestApp.Domain.Services.Abstractions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InventorSoftTestApp.Domain.Services;

public class TaskService(
    ITaskRepository taskRepository,
    IUserRepository userRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : ITaskService
{
    public async Task<TaskDto> Create(TaskDto taskDto)
    {
        var task = mapper.Map<TaskModel>(taskDto);
        var users = await userRepository.FindAll();
        int? index = null;
        if (users.Count > 0)
        {
            var random = new Random();
            index = random.Next(users.Count);
            task.State = TaskState.InProgress;
            task.TransferCounter = 1;
            task.UserId = users[(int)index].Id;
        }
        else
        {
            task.State = TaskState.Waiting;
            task.TransferCounter = 0; 
        }
        
        EntityEntry<TaskModel> result = await taskRepository.InsertAsync(task);

        await unitOfWork.Commit();
        
        if (index.HasValue)
        {
            result.Entity.User = users[index.Value];
        }

        return mapper.Map<TaskDto>(result.Entity);
    }

    public async Task<IReadOnlyCollection<TaskDto>> GetTasks()
    {
        var tasks = await taskRepository.FindAll(task => task.User);

        return mapper.Map<List<TaskDto>>(tasks);
    }
}