using AutoMapper;
using InventorSoftTestApp.Application.Models.Requests.Task;
using InventorSoftTestApp.Application.Models.Requests.User;
using InventorSoftTestApp.Application.Models.Responses.Task;
using InventorSoftTestApp.Application.Models.Responses.User;
using InventorSoftTestApp.Domain.Models.Dtos;

namespace InventorSoftTestApp.Application.Mappings;

public class ApplicationMappingsProfile : Profile
{
    public ApplicationMappingsProfile()
    {
        //request
        CreateMap<CreateUserRequestModel, UserDto>();
        CreateMap<CreateTaskRequestModel, TaskDto>();
        
        //response
        CreateMap<UserDto, UserResponseModel>();
        CreateMap<TaskDto, TaskResponseModel>();
    }
}