using BuildingBlocks.Pagination.Version2.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BuildingBlocks.Pagination.Version2;

//public class SortModel
//{
//    [JsonPropertyName("sort_field")] public string SortField { get; set; } = string.Empty;
//    [JsonPropertyName("sort_direction")] public SortDirection SortDirection { get; set; } // desc, asc
//}

public record SortModel(
    [JsonProperty(PropertyName = "sort_field")] 
    [FromQuery]
    string? SortField,
    [JsonProperty(PropertyName = "sort_direction")] 
    [FromQuery]
    SortDirection? SortDirection
    );


public record SortModelV2(
    [JsonProperty(PropertyName = "sort_field")] 
    [FromQuery]
    string? SortField,
    [JsonProperty(PropertyName = "sort_direction")] 
    [FromQuery]
    string? SortDirection
);