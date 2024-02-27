using InventorSoftTestApp.Application.Models.Responses.Task;

namespace InventorSoftTestApp.Application.Models.Responses.User;

public class UserResponseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IReadOnlyCollection<TaskResponseModel> Tasks { get; set; }
}