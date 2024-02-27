using System.Net;
using InventorSoftTestApp.Domain.Models.Enums;

namespace InventorSoftTestApp.Domain.Exceptions;

public abstract class ApplicationException(
    ErrorCode errorCode,
    HttpStatusCode statusCode,
    string? message) : Exception(message)
{
    public ErrorCode ErrorCodeValue { get; } = errorCode;
    public HttpStatusCode StatusCode { get; } = statusCode;
}