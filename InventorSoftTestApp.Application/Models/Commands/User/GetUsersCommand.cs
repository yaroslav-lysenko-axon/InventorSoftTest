using InventorSoftTestApp.Application.Models.Responses.User;
using MediatR;

namespace InventorSoftTestApp.Application.Models.Commands.User;

public class GetUsersCommand : IRequest<IReadOnlyCollection<UserResponseModel>>
{
    
}