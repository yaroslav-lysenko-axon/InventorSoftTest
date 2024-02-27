using AutoMapper;
using InventorSoftTestApp.Domain.Models.DbEntities;
using InventorSoftTestApp.Domain.Models.Dtos;

namespace InventorSoftTestApp.Domain.Mappings;

public class DomainMappingsProfile : Profile
{
    public DomainMappingsProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<TaskModel, TaskDto>();
        
        CreateMap<UserDto, User>();
        CreateMap<TaskDto, TaskModel>();
    }
}