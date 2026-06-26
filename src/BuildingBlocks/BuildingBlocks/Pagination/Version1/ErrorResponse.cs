using System.Text.Json.Serialization;

namespace BuildingBlocks.Pagination.Version1;

public class ErrorResponse
{
    [JsonPropertyName("error_code")]
    public string ErrorCode { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}