using AutoMapper;
using InventorSoftTestApp.Application.Models.Commands.Task;
using InventorSoftTestApp.Application.Models.Responses.Task;
using InventorSoftTestApp.Domain.Services.Abstractions;
using MediatR;

namespace InventorSoftTestApp.Application.Handlers.Task;

public class GetTasksHandler(
    ITaskService taskService,
    IMapper mapper) : IRequestHandler<GetTasksCommand, IReadOnlyCollection<TaskResponseModel>>
{
    public async Task<IReadOnlyCollection<TaskResponseModel>> Handle(
        GetTasksCommand request,
        CancellationToken cancellationToken)
    {
        var response = await taskService.GetTasks();

        return mapper.Map<List<TaskResponseModel>>(response);
    }
}