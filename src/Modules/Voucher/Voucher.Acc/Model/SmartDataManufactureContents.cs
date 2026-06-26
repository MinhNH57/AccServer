using System.ComponentModel.DataAnnotations;

namespace Voucher.Acc.Model
{
    public class SmartDataManufactureContents
    {
        public Guid? IdContents { get; set; } = Guid.NewGuid();
        public string? DataType { get; set; } = string.Empty;
        public string? DebitSide { get; set; } = string.Empty;
        public string? CreditSide { get; set; } = string.Empty;
        public string? StageCode { get; set; } = string.Empty;
        public string? StageName { get; set; } = string.Empty;
        public string? CommodityCode { get; set; } = string.Empty;
        public string? CommodityName { get; set; } = string.Empty;
        public string? WarehoseCode { get; set; } = string.Empty;
        public string? WarehoseName { get; set; } = string.Empty;
        public string? WarehoseCodeReceive { get; set; } = string.Empty;
        public string? WarehoseNameReceive { get; set; } = string.Empty;
        public string? ProductCode { get; set; } = string.Empty;
        public string? ProductName { get; set; } = string.Empty;
        public string? UnitPcs { get; set; } = string.Empty;
        public string? UnitPackage { get; set; } = string.Empty;
        public double? ConversionFactor { get; set; } = 0;
        public double? PackageQuantity { get; set; } = 0;
        public double? QuantityOfInventory { get; set; } = 0;
        public double? Quantity { get; set; } = 0;
        public double? Weight { get; set; } = 0;
        public double? QuantityNotGood { get; set; } = 0;
        public double? WeightNotGood { get; set; } = 0;
        public double? Price { get; set; } = 0;
        public double? PriceAfterDiscount { get; set; } = 0;
        public double? PriceWithoutVat { get; set; } = 0;
        public decimal? AmountOfMoney { get; set; } = 0;
        public decimal? AmountVat { get; set; } = 0;
        public decimal? AmountWithoutVat { get; set; } = 0;
        public double? VatRate { get; set; } = 0;
        public double? DiscountRate { get; set; } = 0;
        public decimal? AmountDiscount { get; set; } = 0;
        public decimal? AmountAfterDiscount { get; set; } = 0;
        public string? ProjectCode { get; set; } = string.Empty;
        public string? ProjectName { get; set; } = string.Empty;
        public string? VoucherNumberContents { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
        public int? CodeUnit { get; set; } = 0;
        public bool? IsActive { get; set; } = false;
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public string? CreateBy { get; set; } = string.Empty;
        public DateTime? ModifyDate { get; set; } = DateTime.Now;
        public string? ModifyBy { get; set; } = string.Empty;
        public Guid? IdData { get; set; }
        [Key]
        public Guid IdSource { get; set; }
        public Guid? IdVouchers { get; set; } = Guid.Empty;
        public Guid? IdTracing { get; set; } = Guid.Empty;
        public double? Density { get; set; } = 0;
    }

}
