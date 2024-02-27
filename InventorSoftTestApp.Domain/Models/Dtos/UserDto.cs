namespace InventorSoftTestApp.Domain.Models.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IReadOnlyCollection<TaskDto> Tasks { get; set; }
}