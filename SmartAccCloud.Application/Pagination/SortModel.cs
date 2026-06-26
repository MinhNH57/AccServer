using Newtonsoft.Json;
using SmartAccCloud.Application.Commons.Enums;

namespace SmartAccCloud.Application.Pagination;

//public class SortModel
//{
//    [JsonPropertyName("sort_field")] public string SortField { get; set; } = string.Empty;
//    [JsonPropertyName("sort_direction")] public SortDirection SortDirection { get; set; } // desc, asc
//}

public record SortModel(
    [JsonProperty(PropertyName = "sort_field")] string? SortField,
    [JsonProperty(PropertyName = "sort_direction")] SortDirection SortDirection
    );