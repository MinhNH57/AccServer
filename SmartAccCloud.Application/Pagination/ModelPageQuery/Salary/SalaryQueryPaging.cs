namespace SmartAccCloud.Application.Pagination.ModelPageQuery.Salary;
public class SalaryQueryPaging : PaginationRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
