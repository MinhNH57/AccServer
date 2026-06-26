namespace SmartAccCloud.Application.Models.Catalogs.AccountSymbol;

public class AccountSymbolVm
{
    public string AccountSymbol { get; set; }
    public string AccountName { get; set; }
    public int? AccountLevel { get; set; }
    public string? AccountParent { get; set; }
    public string AccountType { get; set; }
    public bool Obligatory { get; set; }
    public string? Notes { get; set; }
}