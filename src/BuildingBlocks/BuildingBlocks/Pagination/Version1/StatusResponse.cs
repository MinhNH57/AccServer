using System.Text.Json.Serialization;

namespace BuildingBlocks.Pagination.Version1;

public class StatusResponse
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("desc")]
    public string Desc { get; set; }
}