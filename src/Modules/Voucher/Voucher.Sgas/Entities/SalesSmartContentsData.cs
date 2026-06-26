using BuildingBlocks.SmartMapper;
using System.ComponentModel.DataAnnotations;
using Voucher.Sgas.Model.Contracts;

namespace Voucher.Sgas.Entities;
public class SalesSmartContentsData : IBaseEntity
{
    [SmartMapIgnore]
    public Guid IdContents { get; set; } 
    public string? DataType { get; set; }
    public string? PumpColumnCode { get; set; }
    public string? PumpColumnName { get; set; }
    public double? NumberBegin { get; set; }
    public double? NumberEnd { get; set; }
    public string? CommodityCode { get; set; }
    public string? CommodityName { get; set; }
    public string? UnitPcs { get; set; }
    public string? UnitPackage { get; set; }
    public double? ConversionFactor { get; set; }
    public double? PackageQuantity { get; set; }
    public double? QuantityOfInventory { get; set; }
    public double? QuantityConsignmentGoods { get; set; }
    public double? QuantityTest { get; set; }
    public double? QuantityInternal { get; set; }
    public double? QuantityContract { get; set; }
    public double? QuantityRetail { get; set; }
    public double? Quantity { get; set; }
    public double? Quantity15 { get; set; }
    public double? Price { get; set; }
    public double? PriceWithoutVat { get; set; }
    public double? PriceAfterDiscount { get; set; }
    public decimal? AmountOfMoney { get; set; }
    public decimal? AmountVat { get; set; }
    public decimal? AmountWithoutVat { get; set; }
    public double? FeeEnvironRate { get; set; }
    public decimal? AmountFeeEnvironRate { get; set; }
    public decimal? AmountWithoutVatFee { get; set; }
    public double? PriceWithoutVatFee { get; set; }
    public double? VatRate { get; set; }
    public double? DiscountRate { get; set; }
    public decimal? AmountDiscount { get; set; }
    public decimal? AmountAfterDiscount { get; set; }
    public decimal? ActualAmount { get; set; }
    public string? Season { get; set; }
    public double? CoefficientVcf { get; set; }
    public double? Temperature { get; set; }
    public double? CoefficientWcf { get; set; }
    public string? VoucherNumberContents { get; set; }
    public string? DescriptionContents { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    [SmartMapIgnore]
    public DateTime CreateDate { get; set; }
    [SmartMapIgnore]
    public string? CreateBy { get; set; }
    [SmartMapIgnore]
    public DateTime? ModifyDate { get; set; }
    [SmartMapIgnore]
    public string? ModifyBy { get; set; }
    public Guid? IdData { get; set; }
    [Key]
    public Guid IdSource { get; set; }
    public string? LOAIPHIEU { get; set; }
    public string? WarehoseCode { get; set; }
    public string? WarehoseName { get; set; }
    public string? WarehoseCodeReceive { get; set; }
    public string? WarehoseNameReceive { get; set; }
    public Guid? IdVouchers { get; set; }
    public Guid? IdTracing { get; set; }
    public double? Priceimp { get; set; }
    public double? RetailPrice { get; set; }
    public bool EnviromentByKg { get; set; }
    public double? CostPrice { get; set; }
    public string? ShipmentNumber { get; set; }
    public DateTime? DateShipment { get; set; }
    public double? BogRate { get; set; }
    public decimal? AmountBog { get; set; }
    public double? ExciseTaxRate { get; set; }
    public decimal? AmountExciseTax { get; set; }
    public string? BogType { get; set; }
    public decimal? AmountTotal { get; set; }
    public int? SmartId { get; set; }
    public string? ShortAddressSupplier { get; set; }
    public string? AddressSupplier { get; set; }
    public string? ShortNameSupplier { get; set; }
    public string? DebitObjectCode { get; set; }
    public string? DebitObjectName { get; set; }
    public string? DebitObjectTax { get; set; }
    public string? RevenueExpenseCode { get; set; }
    public string? RevenueExpenseName { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
    public string? CreditObjectCode { get; set; }
    public string? CreditObjectName { get; set; }
    public string? CreditObjectTax { get; set; }
    public string? EventsContentCode { get; set; }
    public string? EventsContentName { get; set; }
    public string? BatchWarehose { get; set; }
    public double? NumberFuel { get; set; }
    public double? NumberInvPublish { get; set; }
    public bool IsPromotion { get; set; }
    public string? VatType { get; set; }
    public bool ExpPrivateCont { get; set; }
    public string? ReasonCodeCont { get; set; }
    public string? ReasonNameCont { get; set; }
    public bool NoVat { get; set; }
    public bool CreatedInvoice { get; set; }
    public string? IdProgram { get; set; }
    public DateTime? DateExpiration { get; set; }
    public string? ForeignCurrencyType { get; set; }
    public double? ExchangeRate { get; set; }
    public string? UnitScale { get; set; }
    public double? DiscountRate1 { get; set; }
    public double? DiscountRate2 { get; set; }
    public double? DiscountRate3 { get; set; }
    public double? DiscountRate4 { get; set; }
    public double? DiscountRate5 { get; set; }
    public decimal? AmountDiscount1 { get; set; }
    public decimal? AmountDiscount2 { get; set; }
    public decimal? AmountDiscount3 { get; set; }
    public decimal? AmountDiscount4 { get; set; }
    public decimal? AmountDiscount5 { get; set; }
    public string? StorageLocation { get; set; }
    public string? TypeNameRevExp { get; set; }
    public Guid Id { get; set; }
}
