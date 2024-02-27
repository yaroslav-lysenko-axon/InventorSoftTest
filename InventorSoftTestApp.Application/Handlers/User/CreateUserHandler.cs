using AutoMapper;
using InventorSoftTestApp.Application.Models.Commands.User;
using InventorSoftTestApp.Application.Models.Responses.User;
using InventorSoftTestApp.Domain.Models.Dtos;
using InventorSoftTestApp.Domain.Services.Abstractions;
using MediatR;

namespace InventorSoftTestApp.Application.Handlers.User;

public class CreateUserHandler(
    IMapper mapper,
    IUserService userService) : IRequestHandler<CreateUserCommand, UserResponseModel>
{
    public async Task<UserResponseModel> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        UserDto userDto = mapper.Map<UserDto>(request.CreateUserRequestModel);
        var response = await userService.Create(userDto);

        return mapper.Map<UserResponseModel>(response);
    }
}