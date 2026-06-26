using System.Text.Json.Serialization;
using SmartAccCloud.Application.Commons.Enums;

namespace SmartAccCloud.Application.Pagination
{
    public class AggregateModel
    {
        [JsonPropertyName("field_aggregate")] public string FieldAggregate { get; set; } = string.Empty;

        [JsonPropertyName("function_aggregate")]
        public AggregateFunc FunctionAggregate { get; set; }
    }
}