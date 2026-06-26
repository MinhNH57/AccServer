using BuildingBlocks.Pagination.Version2.Enums;


namespace BuildingBlocks.Pagination.Version2;

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
    public int PageSize { get; set; } = 20;
    //public SortModel? SortModels { get; set; }
    public SortModelV2? SortModels { get; set; }
    
    public SearchModel? SearchByFields { get; set; }    
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }


}