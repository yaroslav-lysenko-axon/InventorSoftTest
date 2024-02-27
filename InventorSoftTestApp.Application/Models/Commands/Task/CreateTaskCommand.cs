using InventorSoftTestApp.Application.Models.Requests.Task;
using InventorSoftTestApp.Application.Models.Responses.Task;
using MediatR;

namespace InventorSoftTestApp.Application.Models.Commands.Task;

public class CreateTaskCommand : IRequest<TaskResponseModel>
{
    public CreateTaskRequestModel CreateTaskRequestModel { get; set; }
}