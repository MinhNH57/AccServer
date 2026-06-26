namespace FileHandle.Models;

public class ExcelSmartData
{
    public Guid Id { get; set; }= Guid.NewGuid();

    public string? DataType { get; set; }

    public string? DataName { get; set; }

    public bool? CostAllotment { get; set; }

    public DateTime? VoucherDate { get; set; }

    public DateTime? RecordDate { get; set; }

    public string? NumberOfVouchers { get; set; }

    public string? InvoiceNumber { get; set; }

    public string? PersonCode { get; set; }

    public string? PersonName { get; set; }

    public string? PersonAddress { get; set; }

    public string? PersonTaxCode { get; set; }

    public string? ObjectCode { get; set; }

    public string? ObjectName { get; set; }

    public string? ObjectAddress { get; set; }

    public string? ObjectTaxCode { get; set; }

    public string? WarehoseCode { get; set; }

    public string? WarehoseName { get; set; }

    public string? WarehoseCodeReceive { get; set; }

    public string? WarehoseNameReceive { get; set; }

    public string? ReasonCode { get; set; }

    public string? ReasonName { get; set; }

    public string? ShippingMethodsCode { get; set; }

    public string? ShippingMethodsName { get; set; }

    public string? MethodOfPaymentsCode { get; set; }

    public string? MethodOfPaymentsName { get; set; }

    public string? Description { get; set; }

    public string? Notes { get; set; }

    public bool? Pricing { get; set; }

    public bool? EInvoice { get; set; }

    public bool? InvocePublished { get; set; }

    public string? KeyInvoce { get; set; }

    public string? ENumberInvoice { get; set; }

    public string? ENumberInvoiceDraft { get; set; }

    public string? InvoiceResult { get; set; }

    public Guid? IdDataInherit { get; set; }

    public bool? NotEnvironment { get; set; }

    public string? VehiclesName { get; set; }

    public string? VoucherStatus { get; set; }

    public string? LicensePlates { get; set; }

    public string? InvoiceSymbol { get; set; }

    public string? InvoiceTemplate { get; set; }

    public string? SignTransfer { get; set; }

    public bool? Register { get; set; } 
    public int? CodeUnit { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyBy { get; set; }

    public bool? Selectted { get; set; }

    public bool? InvoiceCancel { get; set; }

    public string? ObjectEmail { get; set; }

    public string? IdDataHead { get; set; }

    public bool? ComfirmVoucher { get; set; }

    public string? VoucherNoInherit { get; set; }

    public string? GroupAreaCode { get; set; }

    public string? GroupAreaName { get; set; }

    public bool? Delivered { get; set; }

    public string? GroupCode { get; set; }

    public string? GroupName { get; set; }

    public string? ContractNo { get; set; }

    public string? RoomCode { get; set; }

    public string? RoomName { get; set; }

    public bool? VAT { get; set; }

    public bool? SaveTemp { get; set; }

    public string? DebitSide { get; set; }

    public string? CreditSide { get; set; }

    public string? AccountSymbol { get; set; }

    public string? CommodityCode { get; set; }

    public string? CommodityName { get; set; }

    public string? ProductCode { get; set; }

    public string? ProductName { get; set; }

    public string? UnitPcs { get; set; }

    public string? UnitPackage { get; set; }

    public double? ConversionFactor { get; set; }

    public double? PackageQuantity { get; set; }

    public double? QuantityOfInventory { get; set; }

    public double? Quantity { get; set; }

    public double? Quantity15 { get; set; }

    public double? RetailPrice { get; set; }

    public double? Price { get; set; }

    public decimal? AmountOfMoney { get; set; }

    public decimal? AmountVat { get; set; }

    public decimal? AmountWithoutVat { get; set; }

    public string? ForeignCurrencyType { get; set; }

    public double? ExchangeRate { get; set; }

    public double? AmountOfMoneyUsd { get; set; }

    public string? VatType { get; set; }

    public double? VatRate { get; set; }

    public double? DiscountRate { get; set; }

    public decimal? AmountDiscount { get; set; }

    public decimal? AmountAfterDiscount { get; set; }

    public double? FeeEnvironRate { get; set; }

    public decimal? AmountFeeEnvironRate { get; set; }

    public double? CostPrice { get; set; }

    public decimal? CostOfGoodsSold { get; set; }

    public string? DebitObjectCode { get; set; }

    public string? DebitObjectName { get; set; }

    public string? DebitObjectTax { get; set; }

    public string? CreditObjectCode { get; set; }

    public string? CreditObjectName { get; set; }

    public string? CreditObjectTax { get; set; }

    public string? InvoiceNumberContents { get; set; }

    public string? RevenueExpenseCode { get; set; }

    public string? RevenueExpenseName { get; set; }

    public string? ContractCode { get; set; }

    public string? ContractName { get; set; }

    public string? ConstructionCode { get; set; }

    public string? ConstructionName { get; set; }

    public string? ProjectCode { get; set; }

    public string? ProjectName { get; set; }

    public string? ProductionActivitieCode { get; set; }

    public string? ProductionActivitieName { get; set; }

    public string? FundingSourceCode { get; set; }

    public string? FundingSourceName { get; set; }

    public string? DebitSideOut { get; set; }

    public string? CreditSideOut { get; set; }

    public double? CoefficientVcf { get; set; }

    public double? Temperature { get; set; }

    public double? CoefficientWcf { get; set; }

    public string? VoucherNumberContents { get; set; }

    public Guid? IdData { get; set; }

    public Guid? IdSource { get; set; }

    public decimal? AmountOfMoney15 { get; set; }

    public decimal? CostOfGoodsSold15 { get; set; }

    public string? Season { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public string? AmountExciseTax { get; set; }

    public string? ExciseTaxRate { get; set; }

    public double? PriceEnd { get; set; }

    public double? BogRate { get; set; }

    public double? AmountBog { get; set; }

    public double? AmountTotal { get; set; }

    public bool? EnviromentByKg { get; set; }

    public string? ShipmentNumber { get; set; }

    public string? GrpName { get; set; }

    public string? GrpCode { get; set; }

    public string? TypeCode { get; set; }

    public string? TypeName { get; set; }

    public string? NameSupplier { get; set; }

    public string? CodeSupplier { get; set; }

    public Guid? IdVouchers { get; set; }

    public Guid? IdTracing { get; set; }

    public string? NumberImport { get; set; }

    public bool IsCreated { get; set; }

    public string? CreatedBy { get; set; }

    public string? TypeData { get; set; }

    public string? UnitPackage1 { get; set; }

    public double? Conversion1 { get; set; }

    public string? UnitPackage2 { get; set; }

    public double? Conversion2 { get; set; }

    public string? UnitPackage3 { get; set; }

    public double? Conversion3 { get; set; }

    public string? UnitPackage4 { get; set; }

    public double? Conversion4 { get; set; }

    public string? UNITPCSSECOND1 { get; set; }

    public string? UNITPCSSECOND2 { get; set; }

    public string? UNITPCSSECOND3 { get; set; }

    public string? UNITPCSSECOND4 { get; set; }
}
 