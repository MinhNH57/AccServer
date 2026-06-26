namespace Supply.API.Application.Models;

public class AuditingLog
{
    public int RefType { get; set; }
    public int Action { get; set; }
    public string ActionName { get; set; }
    public string Reference { get; set; }
    public int State { get; set; }
    public string ObjectName { get; set; }
    public string BranchName { get; set; }
}