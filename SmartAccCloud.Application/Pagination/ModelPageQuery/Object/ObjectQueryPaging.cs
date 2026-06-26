namespace SmartAccCloud.Application.Pagination.ModelPageQuery.Object;

public class ObjectQueryPaging : PaginationRequest
{
    public bool IsStaff { get; set; } = false;
    public bool IsSupplier { get; set; } = false;
    public bool IsCustomer { get; set; } = false;
    public bool IsBank { get; set; } = false;
    public bool IsOther { get; set; } = false;
}