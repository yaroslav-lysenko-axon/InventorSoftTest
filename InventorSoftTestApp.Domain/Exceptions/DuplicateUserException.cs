using System.Net;
using InventorSoftTestApp.Domain.Models.Enums;

namespace InventorSoftTestApp.Domain.Exceptions;

public class DuplicateUserException : ApplicationException
{
    private const string DuplicateUser = "Duplicate username: ";

    public DuplicateUserException(string name)
        : base(ErrorCode.DuplicatedUserException, HttpStatusCode.Conflict, DuplicateUser + name)
    {
    }
}