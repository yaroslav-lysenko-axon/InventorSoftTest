using AutoMapper;
using InventorSoftTestApp.Application.Models.Commands.Task;
using InventorSoftTestApp.Application.Models.Responses.Task;
using InventorSoftTestApp.Domain.Models.Dtos;
using InventorSoftTestApp.Domain.Models.Enums;
using InventorSoftTestApp.Domain.Services.Abstractions;
using MediatR;

namespace InventorSoftTestApp.Application.Handlers.Task;

public class CreateTaskHandler(
    ITaskService taskService,
    IMapper mapper) : IRequestHandler<CreateTaskCommand, TaskResponseModel>
{
    public async Task<TaskResponseModel> Handle(
        CreateTaskCommand request,
        CancellationToken cancellationToken)
    {
        TaskDto taskDto = mapper.Map<TaskDto>(request.CreateTaskRequestModel);
        taskDto.State = TaskState.Waiting;
        
        var response = await taskService.Create(taskDto);

        return mapper.Map<TaskResponseModel>(response);
    }
}