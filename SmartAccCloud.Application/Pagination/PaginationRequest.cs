using Newtonsoft.Json;
using SmartAccCloud.Application.Commons.Enums;

namespace SmartAccCloud.Application.Pagination;

public class PaginationRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public ICollection<SearchModel>? SearchByFields { get; set; }
    public ICollection<SortModel>? SortModels { get; set; }
    public IDictionary<string, AggregateFunc>? AggregateModels { get; set; }
}


public class PaginationMobileRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;

    [JsonProperty(PropertyName = "searchFieldName")]
    public string? SearchFieldName { get; set; } 

    [JsonProperty(PropertyName = "searchValue")]
    public string? SearchValue { get; set; } 

    [JsonProperty(PropertyName = "sortField")]
    public string? SortField { get; set; } 

    [JsonProperty(PropertyName = "sortDirection")]
    public SortDirection SortDirection { get; set; } // desc, asc
}