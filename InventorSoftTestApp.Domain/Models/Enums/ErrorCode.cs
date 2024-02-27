using System.ComponentModel.DataAnnotations;

namespace InventorSoftTestApp.Domain.Models.Enums;

public enum ErrorCode
{
    [Display(Name = "duplicatedUserException")]
    DuplicatedUserException,
    [Display(Name = "validationFailed")]
    ValidationFailed,
}