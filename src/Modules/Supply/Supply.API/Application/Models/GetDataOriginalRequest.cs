namespace Supply.API.Application.Models;

public class GetDataOriginalRequest
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string AccountList { get; set; }
    public int DisplayOnBook { get; set; }
    public int Period { get; set; }
    public int RefType { get; set; }
    public Guid? SupplyId { get; set; }
    public List<Guid> ListRefId { get; set; }
}