using System.Text.Json.Serialization;

namespace BuildingBlocks.Pagination.Version1;

public class MetaResponse
{
    [JsonPropertyName("pagination")]
    public PaginationResponse Pagination { get; set; }

    [JsonPropertyName("sorting")]
    public List<Sort> Sorting { get; set; }
}