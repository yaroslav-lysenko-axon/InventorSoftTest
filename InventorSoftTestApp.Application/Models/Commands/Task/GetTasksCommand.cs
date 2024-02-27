using InventorSoftTestApp.Application.Models.Responses.Task;
using MediatR;

namespace InventorSoftTestApp.Application.Models.Commands.Task;

public class GetTasksCommand : IRequest<IReadOnlyCollection<TaskResponseModel>>
{
    
}