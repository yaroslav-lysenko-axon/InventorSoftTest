using AutoMapper;
using InventorSoftTestApp.Domain.Exceptions;
using InventorSoftTestApp.Domain.Models.DbEntities;
using InventorSoftTestApp.Domain.Models.Dtos;
using InventorSoftTestApp.Domain.Repositories.Abstractions;
using InventorSoftTestApp.Domain.Services.Abstractions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InventorSoftTestApp.Domain.Services;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IUserService
{
    public async Task<UserDto> Create(UserDto userDto)
    {
        var foundUsers = await userRepository.Find(user => user.Name == userDto.Name);
        if (foundUsers != null && foundUsers.Any())
        {
            throw new DuplicateUserException(userDto.Name);
        }
        
        var user = mapper.Map<User>(userDto);
        EntityEntry<User> result = await userRepository.InsertAsync(user);

        await unitOfWork.Commit();
        return mapper.Map<UserDto>(result.Entity);
    }

    public async Task<IReadOnlyCollection<UserDto>> GetUsers()
    {
        var users = await userRepository.FindAll(user => user.Tasks);

        return mapper.Map<List<UserDto>>(users);
    }
}