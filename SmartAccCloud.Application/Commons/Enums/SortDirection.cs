using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SmartAccCloud.Application.Commons.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortDirection
{
    [EnumMember(Value = "asc")] asc,
    [EnumMember(Value = "desc")] desc
}