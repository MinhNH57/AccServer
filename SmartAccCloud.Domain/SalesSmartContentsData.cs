using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SmartAccCloud.Domain;
public class SalesSmartContentsData
{
    public Guid IdContents { get; set; }
    //[Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //public int IdAsc { get; set; }
    public string? DataType { get; set; }
    public double NumberBegin { get; set; }
    public double NumberEnd { get; set; }
    public string? CommodityCode { get; set; }
    public string? CommodityName { get; set; }
    public string? UnitPcs { get; set; }
    public string? UnitPackage { get; set; }
    public double ConversionFactor { get; set; }
    public double PackageQuantity { get; set; }
    public double QuantityOfInventory { get; set; }
    public double QuantityRetail { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
    public double PriceWithoutVat { get; set; }
    public double PriceAfterDiscount { get; set; }
    [Precision(18, 2)]
    public decimal AmountOfMoney { get; set; }
    [Precision(18, 2)]
    public decimal AmountVat { get; set; }
    [Precision(18, 2)]
    public decimal AmountWithoutVat { get; set; }
    public double FeeEnvironRate { get; set; }
    [Precision(18, 2)]
    public decimal AmountFeeEnvironRate { get; set; }
    [Precision(18, 2)]
    public decimal AmountWithoutVatFee { get; set; }
    public double PriceWithoutVatFee { get; set; }
    public double VatRate { get; set; }
    public double DiscountRate { get; set; }
    [Precision(18, 2)]
    public decimal AmountDiscount { get; set; }
    [Precision(18, 2)]
    public decimal AmountAfterDiscount { get; set; }
    [Precision(18, 2)]
    public decimal ActualAmount { get; set; }
    public string? VoucherNumberContents { get; set; }
    public string? DescriptionContents { get; set; }
    public string? Notes { get; set; }
    public int CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public DateTime ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public Guid IdData { get; set; }
    [Key]
    public Guid IdSource { get; set; }
    public string? WarehoseCode { get; set; }
    public string? WarehoseName { get; set; }
    public string? WarehoseCodeReceive { get; set; }
    public string? WarehoseNameReceive { get; set; }
    public Guid IdVouchers { get; set; }
    //public Guid IdTracing { get; set; }
    public double Priceimp { get; set; }
    public double RetailPrice { get; set; }
    public string? ShipmentNumber { get; set; }
    [Precision(18, 2)]
    public decimal AmountExciseTax { get; set; }
    public string? BogType { get; set; }
    [Precision(18, 2)]
    public decimal AmountTotal { get; set; }
    // public int SmartId { get; set; }
    public string? ShortAddressSupplier { get; set; }
    // public string? AddressSupplier { get; set; }
    // public string? ShortNameSupplier { get; set; }
    public string? DebitObjectCode { get; set; }
    public string? DebitObjectName { get; set; }
    // public string? DebitObjectTax { get; set; }
    public string? RevenueExpenseCode { get; set; }
    public string? RevenueExpenseName { get; set; }
    public string? ProductName { get; set; }
     public string? ProductCode { get; set; }
     public string? CreditObjectCode { get; set; }
    public string? CreditObjectName { get; set; }
    // public string? CreditObjectTax { get; set; }
    // public string? EventsContentCode { get; set; }
    // public string? EventsContentName { get; set; }
    // public string? BatchWarehose { get; set; }
}
