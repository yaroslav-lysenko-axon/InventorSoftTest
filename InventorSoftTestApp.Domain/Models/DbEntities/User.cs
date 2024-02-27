namespace InventorSoftTestApp.Domain.Models.DbEntities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<TaskModel> Tasks { get; set; }
}