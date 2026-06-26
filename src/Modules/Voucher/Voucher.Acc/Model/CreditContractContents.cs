using System.ComponentModel.DataAnnotations.Schema;

namespace Voucher.Acc.Model;

public class CreditContractContents
{
    public Guid IdContents { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public string? DataType { get; set; }
    public Guid IdSource { get; set; }=Guid.NewGuid();
    public int? CodeUnit { get; set; }
    public string? FundingSourceCode { get; set; }
    public string? FundingSourceName { get; set; }
    public decimal? AmountOfMoney { get; set; }
    public decimal? AmountOfMoneyExchange { get; set; }
    public string? Notes { get; set; }
    public string? DebitSide { get; set; }
    public string? CreditSide { get; set; }
    public string? AccountSymbol { get; set; }
    public string? CommodityCode { get; set; }
    public string? CommodityName { get; set; }
    public string? WarehoseCode { get; set; }
    public string? WarehoseName { get; set; }
    public string? WarehoseCodeReceive { get; set; }
    public string? WarehoseNameReceive { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public string? CodeUnitPcs { get; set; }
    public string? UnitPcs { get; set; }
    public string? CodeUnitPackage { get; set; }
    public string? UnitPackage { get; set; }
    public double? ConversionFactor { get; set; }
    public double? PackageQuantity { get; set; }
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
    public string? VoucherModeCode { get; set; }
    public string? VoucherModeName { get; set; }
    public string? ObjectTHCPCode { get; set; }
    public string? ObjectTHCPName { get; set; }
    public string? MaterialsCode { get; set; }
    public string? MaterialsName { get; set; }
    public string? AccountAllotmentCode { get; set; }
    public string? AccountAllotmentName { get; set; }
    public string? AccountingShortcutsCode { get; set; }
    public string? AccountingShortcutsContents { get; set; }
    public string? CreditObjectCode { get; set; }
    public string? CreditObjectName { get; set; }
    public string? DebitObjectCode { get; set; } 
    public string? DebitObjectName { get; set; }
    public decimal? DeferredLcFee { get; set; }
    public decimal? LcConfirmationFee { get; set; } 
    public decimal? InsuranceFee { get; set; }
    public decimal? GuaranteeFee { get; set; }
    public string? InvoiceNumberContents { get; set; } = string.Empty;
    public string? VoucherNumberContents { get; set; } = string.Empty;
}
