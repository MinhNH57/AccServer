namespace Catalog.Base.GenarateCode.Models;

public class GroupCodeQuery
{
    public string? Parameter { get; set; } = string.Empty;
    public string? UserCode { get; set; } = string.Empty;
    public int? CodeUnit { get; set; } = 0;
    public string? TableName { get; set; } = string.Empty;
    public string? SourceCode { get; set; } = string.Empty;
    public string? DestinationCode { get; set; } = string.Empty;
    public bool IsDelete { get; set; }
}