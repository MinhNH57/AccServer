using Newtonsoft.Json;

namespace BuildingBlocks.Pagination.Version2;

//public class SearchModel
//{
//    [JsonPropertyName("search_field_name")]
//    public string SearchFieldName { get; set; } = string.Empty;

//    [JsonPropertyName("search_value")] public string? SearchValue { get; set; } = string.Empty;
//    //[JsonPropertyName("match_type")]
//    //public bool MatchType { get; set; } = MatchTypes.Contain;
//}

public record SearchModel(
    [JsonProperty(PropertyName = "search_field_name")] string? SearchFieldName,
    [JsonProperty(PropertyName = "search_value")] string? SearchValue
    //[JsonProperty(PropertyName = "search_direction")] string? SearchDirection
    );