using InventorSoftTestApp.Application.Models.Responses.User;
using InventorSoftTestApp.Domain.Models.Enums;

namespace InventorSoftTestApp.Application.Models.Responses.Task;

public class TaskResponseModel
{
    public int Id { get; set; } 
    public int? UserId { get; set; }
    public string Description { get; set; }
    public TaskState State { get; set; }
    public int TransferCounter { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserResponseModel User { get; set; }
}