using InventorSoftTestApp.Domain.Models.Enums;

namespace InventorSoftTestApp.Domain.Models.DbEntities;

public class TaskModel
{
    public int Id { get; set; } 
    public int? UserId { get; set; }
    public string Description { get; set; }
    public TaskState State { get; set; }
    
    public int TransferCounter { get; set; }
    public DateTime CreatedAt { get; set; }
    public User User { get; set; }
}