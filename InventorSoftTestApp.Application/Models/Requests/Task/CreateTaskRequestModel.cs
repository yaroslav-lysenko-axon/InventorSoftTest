using System.ComponentModel.DataAnnotations;

namespace InventorSoftTestApp.Application.Models.Requests.Task;

public class CreateTaskRequestModel
{
    [Required, MinLength(1), MaxLength(2000)]
    public string Description { get; set; }
}