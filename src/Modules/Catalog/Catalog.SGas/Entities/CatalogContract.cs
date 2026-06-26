using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Catalog.SGas.Entities
{
    public class CatalogContract
    {
        public string? GrpCode { get; set; }
        public string? GrpName { get; set; }
        [Key]
        [Required(ErrorMessage = "Mã dự án không được để trống")]

        public string ContractNumber { get; set; } = null!;
        public string? ContentContract { get; set; }
        public DateTime? SigningDate { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Precision(16, 2)]
        public decimal? ValueContract { get; set; }
        [Precision(18, 0)]
        public decimal? ValueContractRate { get; set; }
        [Precision(18, 0)]
        public decimal? LiquidationPrice { get; set; }
        [Precision(18, 0)]
        public decimal? ConversionLiquidationValue { get; set; }
        [Precision(18, 0)]
        public decimal? MaintenanceValue { get; set; }
        public string? ReferenceNumber { get; set; }
        public DateTime? GuaranteeDate { get; set; }
        public DateTime? PaymentDeadline { get; set; }
        public DateTime? DeliveryDeadline { get; set; }
        [Precision(16, 2)]
        public decimal? GuaranteeValue { get; set; }
        public string? CodePartner { get; set; }
        public string? NamePartner { get; set; }
        public string? CodeManage { get; set; }
        public string? NameManage { get; set; }
        public string? ContactName { get; set; }
        public string? CodeManage1 { get; set; }
        public string? NameManage1 { get; set; }
        public bool IsEnd { get; set; }
        public string? CurrencyType { get; set; }
        [Precision(16, 2)]
        public decimal? ExchangeRate { get; set; }
        public string? Notes { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public bool IsActive { get; set; }
        public string? ProductActivCode { get; set; }
        public string? ProductActivName { get; set; }
        public DateTime? MaintenanceDate { get; set; }
        public int? MonthsMaintenance { get; set; }
        [Precision(16, 2)]
        public decimal? ValuesMaintenance { get; set; }
        public bool IsPayed { get; set; }
        public string? ProjectCode { get; set; }
        public string? ProjectName { get; set; }
        public string? RoomCode { get; set; }
        public string? RoomName { get; set; }
        public string? Implementer { get; set; }
        public string? ImplementerCode { get; set; }
        public string? StatusContract { get; set; }
        public DateTime? CancellationDate { get; set; }
        public DateTime? PayDate { get; set; }
        public bool? IsCalculateCost { get; set; }
        public bool? IsPurchaseContract { get; set; }
        public bool? IsInvoice { get; set; }
        public bool? IsSalesContract { get; set; }
        public bool? IsCreditContract { get; set; }
        public string? Reason { get; set; }
        public string? OtherConditions { get; set; }
        public string? ConstructionCode { get; set; }
        public string? ConstructionName { get; set; }
        public int? InterestPaymentType { get; set; }
        public int? OriginalPaymentType { get; set; }
        public string? InterestRateType { get; set; }
        public string? DailyInterestBasis { get; set; }
        public string? AdjustmentMethod { get; set; }
        public string? ContractBorrow { get; set; }
    }
}
