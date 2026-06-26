namespace SmartAccCloud.Application.Models.SmartData;

public class SmartContentDataVm : ICloneable
{
    public Guid IdContents { get; set; }
    public string? DataType { get; set; }
    public string? DebitSide { get; set; }
    public string? CreditSide { get; set; }
    public string? AccountSymbol { get; set; }
    public string? CommodityCode { get; set; }
    public string? CommodityName { get; set; }
    public string? WarehoseCode { get; set; }
    public string? WarehoseName { get; set; }
    public string? UnitPcs { get; set; }
    public string? UnitPackage { get; set; }
    public double? ConversionFactor { get; set; }
    public double? PackageQuantity { get; set; }
    public double? QuantityOfInventory { get; set; }
    public double? Quantity { get; set; }
    public double? RetailPrice { get; set; }
    public double? Price { get; set; }
    public decimal? AmountOfMoney { get; set; }
    public decimal? AmountVat { get; set; }
    public decimal? AmountWithoutVat { get; set; }
    public string? VatType { get; set; }
    public double? VatRate { get; set; }
    public double? FeeEnvironRate { get; set; }
    public decimal? CostOfGoodsSold { get; set; }
    public string? DebitObjectCode { get; set; }
    public string? DebitObjectName { get; set; }
    public string? DebitObjectTax { get; set; }
    public string? CreditObjectCode { get; set; }
    public string? CreditObjectName { get; set; }
    public string? CreditObjectTax { get; set; }
    public string? Description { get; set; }
    public string? RevenueExpenseCode { get; set; }
    public string? RevenueExpenseName { get; set; }
    public string? ProjectCode { get; set; }
    public string? ProjectName { get; set; }
    public string? RoomCode { get; set; }
    public string? RoomName { get; set; }
    public double? CoefficientVcf { get; set; }
    public double? Temperature { get; set; }
    public double? CoefficientWcf { get; set; }
    public double? Density { get; set; }
    public double? AmountExciseTax { get; set; }
    public double? ExciseTaxRate { get; set; }
    public double? BogRate { get; set; }
    public double? AmountBog { get; set; }
    public double? AmountTotal { get; set; }
    public string? VoucherNumberContents { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public Guid? IdData { get; set; }
    public Guid IdSource { get; set; }
    public decimal? CostOfGoodsSold15 { get; set; }
    public bool EnviromentByKg { get; set; }

    public string? ShipmentNumber { get; set; }

    //public Guid IdVouchers {get;set;}
    public Guid? IdTracing { get; set; }
    public DateTime? DateShipment { get; set; }
    public string? WarehoseData { get; set; }

    public object Clone()
    {
        var smartContent = new SmartContentDataVm()
        {
            AccountSymbol = AccountSymbol,
            Notes = Notes,
            IdSource = IdSource,
            AmountBog = AmountBog,
            AmountExciseTax = AmountExciseTax,
            AmountOfMoney = AmountOfMoney,
            AmountTotal = AmountTotal,
            AmountVat = AmountVat,
            AmountWithoutVat = AmountWithoutVat,
            BogRate = BogRate,
            CodeUnit = CodeUnit,
            CoefficientVcf = CoefficientVcf,
            CoefficientWcf = CoefficientWcf,
            CommodityCode = CommodityCode,
            CommodityName = CommodityName,
            ConversionFactor = ConversionFactor,
            CostOfGoodsSold = CostOfGoodsSold,
            CostOfGoodsSold15 = CostOfGoodsSold15,
            CreateBy = CreateBy,
            CreateDate = CreateDate,
            CreditObjectCode = CreditObjectCode,
            CreditObjectName = CreditObjectName,
            CreditObjectTax = CreditObjectTax,
            CreditSide = CreditSide,
            DebitSide = DebitSide,
            DataType = DataType,
            DateShipment = DateShipment,
            WarehoseCode = WarehoseCode,
            WarehoseData = WarehoseData,
            WarehoseName = WarehoseName,
            DebitObjectCode = DebitObjectCode,
            DebitObjectName = DebitObjectName,
            UnitPackage = UnitPackage,
        };
        return smartContent;
    }
}