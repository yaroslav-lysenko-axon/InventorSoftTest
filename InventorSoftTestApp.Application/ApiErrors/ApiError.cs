namespace InventorSoftTestApp.Application.ApiErrors;

public class ApiError
{
    public string? OperationId { get; set; }
    public string Code { get; set; }
    public string Message { get; set; }
    public string Target { get; set; }
    public List<ErrorDetail> Details { get; set; }
    public string StackTrace { get; set; }
}