using System.ComponentModel.DataAnnotations;

namespace InventorSoftTestApp.Application.Models.Requests.User;

public class CreateUserRequestModel
{
    [Required, MinLength(1), MaxLength(50)]
    public string Name { get; set; }
}