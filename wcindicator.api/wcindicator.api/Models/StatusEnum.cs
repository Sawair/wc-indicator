using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace wcindicator.api.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusEnum
    {
        Free,
        Occupied,
        Wait
    }
}