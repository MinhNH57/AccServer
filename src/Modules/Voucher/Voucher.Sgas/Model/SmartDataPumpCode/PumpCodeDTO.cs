using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voucher.Sgas.Model.SmartDataPumpCode;

public class PumpCodeDTO
{
    public Guid Id { get; set; }
    public double? KeyData { get; set; }
    public string NozzleId { get; set; }
    public DateTime? RecordDate { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string PumpNozzleCode { get; set; }
    public string PumpNozzleName { get; set; }
    public string PumpColumnCode { get; set; }
    public string PumpColumnName { get; set; }
    public string CodeWarehose { get; set; }
    public string NameWarehose { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public double? QuantityOfInventory { get; set; }
    public double? Quantity { get; set; }
    public double? CoefficientVcf { get; set; }
    public double? Quantity15 { get; set; }
    public double? RetailPrice { get; set; }
    public double? Price { get; set; }
    public double? PriceWithoutVat { get; set; }
    public double? PriceAfterDiscount { get; set; }
    public decimal? AmountOfMoney { get; set; }
    public decimal? AmountVat { get; set; }
    public decimal? AmountWithoutVat { get; set; }
    public decimal? AmountWithoutVatFee { get; set; }
    public string VatType { get; set; }
    public double? VatRate { get; set; }
    public double? FeeEnvironRate { get; set; }
    public decimal? AmountFeeEnvironRate { get; set; }
    public double? DiscountRate { get; set; }
    public decimal? AmountDiscount { get; set; }
    public decimal? AmountAfterDiscount { get; set; }
    public DateTime CreateDate { get; set; }
    public int? CompanyId { get; set; }
    public string StationId { get; set; }
    public int? DeviceId { get; set; }
    public string Signature { get; set; }
    public bool InvocePublished { get; set; }
    public string KeyInvoce { get; set; }
    public bool IsActive { get; set; }
    public bool IsCreateInv { get; set; }
    public Guid? IdInv { get; set; }
    public string InvoiceNumber { get; set; }
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; }
    public string Notes { get; set; }
    public Guid? IdData { get; set; }
    public string SaleNumber { get; set; }
    public bool NotEnvironment { get; set; }
    public string PersonName { get; set; }
    public string ObjectCode { get; set; }
    public string ObjectName { get; set; }
    public string ObjectTaxCode { get; set; }
    public string ObjectAddress { get; set; }
    public string ObjectEmail { get; set; }
    public string UnitPcs { get; set; }
    public string ReasonCode { get; set; }
    public string ReasonName { get; set; }
    public string MethodOfPaymentsCode { get; set; }
    public string MethodOfPaymentsName { get; set; }
    public string NumberOfVouchers { get; set; }
    public bool EInvoice { get; set; }
    public bool IsExpCheck { get; set; }
    public bool IsHide { get; set; }
    public bool RequirePublish { get; set; }
    public bool ExpPrivate { get; set; }
    public bool NoInvoice { get; set; }
    public string InvoiceSymbol { get; set; }
    public string TransactionID { get; set; }
    public string BatchWarehose { get; set; }
    public string BatchId { get; set; }
    public DateTime? BatchDate { get; set; }
    public string BatchNo { get; set; }
    public string BankOfAmount { get; set; }
    public string BankOfName { get; set; }
    public string BankOfCode { get; set; }
    public string AccountOwner { get; set; }

    public string LicensePlates { get; set; }
    public string Status
    {
        get
        {
            if (ExpPrivate) return "Xuất nội bộ";
            if (IsExpCheck) return "Xuất thử";
            if (InvocePublished) return "Đã phát hành";
            if (IsCreateInv) return "Đã tạo hóa đơn";
            return null;
        }
    }
}
