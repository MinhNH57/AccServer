using System.Text.Json.Serialization;

namespace BuildingBlocks.Pagination.Version1;

public class PaginationResponse
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("total")]
    public long Total { get; set; }

    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }

    [JsonPropertyName("page_count")]
    public int PageCount => (int)Math.Ceiling(Total / (double)PageSize);
}