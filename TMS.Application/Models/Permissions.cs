using System.Text.Json.Serialization;

namespace TMS.Application.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Permissions
    {
        Task_Create,
        Task_Update,
        Task_Delete
    }
}
