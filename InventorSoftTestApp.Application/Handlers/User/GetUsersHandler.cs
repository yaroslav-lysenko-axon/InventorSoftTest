using AutoMapper;
using InventorSoftTestApp.Application.Models.Commands.User;
using InventorSoftTestApp.Application.Models.Responses.User;
using InventorSoftTestApp.Domain.Services.Abstractions;
using MediatR;

namespace InventorSoftTestApp.Application.Handlers.User;

public class GetUsersHandler(
    IUserService userService,
    IMapper mapper) : IRequestHandler<GetUsersCommand, IReadOnlyCollection<UserResponseModel>>
{
    public async Task<IReadOnlyCollection<UserResponseModel>> Handle(
        GetUsersCommand request,
        CancellationToken cancellationToken)
    {
        var response = await userService.GetUsers();
        return mapper.Map<List<UserResponseModel>>(response);
    }
}