using System.Text.Json.Serialization;
using BuildingBlocks.Pagination.Version2.Enums;

namespace BuildingBlocks.Pagination.Version2
{
    public class AggregateModel
    {
        [JsonPropertyName("field_aggregate")] public string FieldAggregate { get; set; } = string.Empty;

        [JsonPropertyName("function_aggregate")]
        public AggregateFunc FunctionAggregate { get; set; }
    }
}