using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InventorSoftTestApp.Domain.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TaskState
    {
        Waiting,
        InProgress,
        Completed
    }
}