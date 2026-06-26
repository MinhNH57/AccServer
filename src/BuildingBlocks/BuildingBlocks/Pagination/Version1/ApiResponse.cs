using System.Text.Json.Serialization;

namespace BuildingBlocks.Pagination.Version1;

public class ApiResponse<T>
{
    [JsonPropertyName("status")]
    public StatusResponse Status { get; set; }

    [JsonPropertyName("data")]
    public T Data { get; set; }

    [JsonPropertyName("error")]
    public ErrorResponse Error { get; set; }

    [JsonPropertyName("links")]
    public LinksResponse Links { get; set; }

    [JsonPropertyName("meta")]
    public MetaResponse Meta { get; set; }
}
