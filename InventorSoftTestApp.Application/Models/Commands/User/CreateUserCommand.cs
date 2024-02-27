using InventorSoftTestApp.Application.Models.Requests.User;
using InventorSoftTestApp.Application.Models.Responses.User;
using MediatR;

namespace InventorSoftTestApp.Application.Models.Commands.User;

public class CreateUserCommand : IRequest<UserResponseModel>
{
    public CreateUserRequestModel CreateUserRequestModel { get; set; }
}