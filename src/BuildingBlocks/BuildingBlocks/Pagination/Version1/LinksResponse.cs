using System.Text.Json.Serialization;

namespace BuildingBlocks.Pagination.Version1;

public class LinksResponse
{
    [JsonPropertyName("self")]
    public string? Self { get; set; }

    [JsonPropertyName("first")]
    public string? First { get; set; }

    [JsonPropertyName("next")]
    public string? Next { get; set; }

    [JsonPropertyName("prev")]
    public string? Prev { get; set; }

    [JsonPropertyName("last")]
    public string? Last { get; set; }
}