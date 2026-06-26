namespace SmartAccCloud.Application.Models.Catalogs.AccountCostSold;

public class AccountCostSoldView
{
    public string? DebitSide { get; set; }
    public string? CreditSide { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public Guid Identifier { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public Guid Id { get; set; }
}