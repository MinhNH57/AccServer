namespace Catalog.SGas.Entities;

public class OpenAccounting
{
    public string? AccountSymbol { get; set; }
    public decimal? OpeningDebit { get; set; }
    public decimal? OpeningCredit { get; set; }
    public decimal? AmountOfMoney { get; set; }
    public string? ForeignCurrencyType { get; set; }
    public double? ExchangeRate { get; set; }
    public double? AmountOfMoneyUsd { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? WarehoseCode { get; set; }
    public string? WarehoseName { get; set; }
    public string? CommodityCode { get; set; }
    public string? CommodityName { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? RevenueExpenseCode { get; set; }
    public string? RevenueExpenseName { get; set; }
    public string? ContractCode { get; set; }
    public string? ContractName { get; set; }
    public string? ConstructionCode { get; set; }
    public string? ConstructionName { get; set; }
    public string? ProjectCode { get; set; }
    public string? ProjectName { get; set; }
    public string? RoomCode { get; set; }
    public string? RoomName { get; set; }
    public string? ProductionActivitieCode { get; set; }
    public string? ProductionActivitieName { get; set; }
    public string? FundingSourceCode { get; set; }
    public string? FundingSourceName { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public int? CodeUnit { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public decimal? DebtBalancMonthTowMonth { get; set; }
    public decimal? DebtBalancTowThreeMonth { get; set; }
    public decimal? DebtBalancThreeSixMonth { get; set; }
    public decimal? DebtBalancGreaterSixMonth { get; set; }
}
