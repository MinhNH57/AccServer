namespace SmartAccCloud.Application.Models.QueryModels.CatalogObject;
public class QueryObjectSalary
{
    public bool StatusJob { get; set; }
    public bool IsAddNewStaff { get; set; }
    public List<string> LstCodeRoom { get; set; } = new();

}
