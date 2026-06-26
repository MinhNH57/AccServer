using System.ComponentModel.DataAnnotations;
using Voucher.Acc.Model.Contracts;

namespace Voucher.Acc.Model
{
    public class SalesSmartContentsData :IBaseEntity
    {
        public Guid? IdContents { get; set; } = Guid.NewGuid();
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
        public bool? IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyBy { get; set; }
        public Guid? IdData { get; set; }
        [Key]
        public Guid? IdSource { get; set; } = Guid.NewGuid();
        public string? LOAIPHIEU { get; set; }
        public Guid? IdVouchers { get; set; }
        public Guid? IdTracing { get; set; }
        public double? Priceimp { get; set; }
        public double? RetailPrice { get; set; }
        public bool? EnviromentByKg { get; set; }
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
        public string? VatType { get; set; }
        public bool? IsPromotion { get; set; }
        public bool? ExpPrivateCont { get; set; }
        public string? ReasonCodeCont { get; set; }
        public string? ReasonNameCont { get; set; }
        public bool? CreatedInvoice { get; set; }
        public bool? NoVat { get; set; }
        public string? ProgramCode { get; set; }
        public bool NotPoints { get; set; }
        public string? GrpName { get; set; }
        public string? GrpCode { get; set; }
        public string? TypeCode { get; set; }
        public string? TypeName { get; set; }
        public string? NameSupplier { get; set; }
        public string? CodeSupplier { get; set; }
        public string? WarehoseCode { get; set; }
        public string? WarehoseName { get; set; }
        public string? WarehoseCodeReceive { get; set; }
        public string? WarehoseNameReceive { get; set; }
        public string? DebitSide { get; set; }
        public string? CreditSide { get; set; }
        public string? AccountSymbol { get; set; }
        public bool? KM { get; set; }
        public int? AmountImp { get; set; }
        public double? Weight { get; set; }
        public double? Density { get; set; }
        public double? QuantitySold { get; set; } 
        public double? ExportedQuantity { get; set; } 
        public double? PriceForeignCurrency { get; set; } 
        public double? AmountOfMoneyUsd { get; set; }
        public double? PriceInclucesTax { get; set; } = 0;
        public double? TotalAmountAfterTax { get; set; } = 0;
        public double? PackagePrice { get; set; } = 0;
        public double? QuantityReceived { get; set; } = 0;
        public string? AccountingShortcutsCode { get; set; }= "";
        public string? AccountingShortcutsContents { get; set; } = "";
        //màu sắc,size
        public string? ProductSize { get; set; }
        public string? ProductColor { get; set; }
        //biện pháp xử lý
        public string? TreatmentMeasures { get; set; }
        //Thông báo tới
        public string? NotifyTo { get; set; }
        //Phản hồi
        public string? FeedBack { get; set; }
        //Hình thức
        public string? Method { get; set; }
        //Kết quá
        public string? Result { get; set; }
        //Số xe vận chuyển
        public string? VehicleNumber { get; set; }
        //Số bill
        public string? BillNumber { get; set; }
        //Số cont
        public int? ContainerCount { get; set; }
        //Loại cont
        public string? ContainerType { get; set; }
        //Số tờ khai hải quan
        public string? DeclarationNumber { get; set; }
        //Chi phí giao nhận, vận chuyển
        public string? DeliveryCostType { get; set; }
        //Đã xác nhận
        public bool? IsConfirmed { get; set; }
        //Thông số kỹ thuật
        public string? Specifications { get; set; }
        //Tên nước ngoài
        public string? ForeignName { get; set; }
        //HsCode
        public string? HsCode { get; set; }
        //Xuất xứ
        public string? PlaceOfOrigin { get; set; }
        //Người theo dõi/Phụ trách KDVT
        public string? KDVTFollowerUserId { get; set; }
        //Người kiểm tra/báo cáo
        public string? ReportedBy { get; set; }
        //Số lượng thủ kho
        public int WHCountedQty { get; set; } = 0;
        public string? ContractNumber { get; set; }
        public string? Erp { get; set; }
        public string? CommodityParent { get; set; }
        public bool IsParent { get; set; }
        public string? InvoiceNumberContents { get; set; } = string.Empty;
    }
}
