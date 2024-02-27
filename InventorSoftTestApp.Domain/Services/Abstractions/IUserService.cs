using InventorSoftTestApp.Domain.Models.Dtos;

namespace InventorSoftTestApp.Domain.Services.Abstractions;

public interface IUserService
{
    Task<UserDto> Create(UserDto userDto);
    Task<IReadOnlyCollection<UserDto>> GetUsers();
}