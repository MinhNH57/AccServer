using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities
{
    public class CatalogItems
    {
        public string? GrpCode { get; set; }
        public string? GrpName { get; set; }
        [Key]
        public string ContractNumber { get; set; }
        public string? ContentContract { get; set; }
        public DateTime? SigningDate { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? ValueContract { get; set; }
        public string? ReferenceNumber { get; set; }
        public DateTime? GuaranteeDate { get; set; }
        public decimal? GuaranteeValue { get; set; }
        public string? CodePartner { get; set; }
        public string? NamePartner { get; set; }
        public string? CodeManage { get; set; }
        public string? NameManage { get; set; }
        public string? CodeManage1 { get; set; }
        public string? NameManage1 { get; set; }
        public bool IsEnd { get; set; }
        public string? CurrencyType { get; set; }
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
        public decimal? ValuesMaintenance { get; set; }
        public bool IsPayed { get; set; }
        public string? ContactName { get; set; }
        public string? ProjectCode { get; set; }
        public string? ProjectName { get; set; }
        public string? RoomCode { get; set; }
        public string? RoomName { get; set; }
        public string? Implementer { get; set; }
        public string? StatusContract { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string? ImplementerCode { get; set; }
        public decimal? ValueContractRate { get; set; }
        public decimal? LiquidationPrice { get; set; }
        public decimal? ConversionLiquidationValue { get; set; }
        public decimal? MaintenanceValue { get; set; }
        public DateTime? PaymentDeadline { get; set; }
        public DateTime? DeliveryDeadline { get; set; }
        public string? Reason { get; set; }
        public string? OtherConditions { get; set; }
        public bool? IsCalculateCost { get; set; }
        public bool? IsInvoice { get; set; }
        public bool? IsSales { get; set; }
        public bool? IsPurchase { get; set; }

        public bool? IsSalesContract { get; set; }
        public bool? IsPurchaseContract { get; set; }
        public string? ConstructionCode { get; set; }
        public string? ConstructionName { get; set; }
        public string? Procedure { get; set; }
        public string? ItemsAddress { get; set; }
        public string? ShortAddress { get; set; }
        public string? LevelConstruction { get; set; }
        public string? BiddingPackage { get; set; }
        public Decimal? CapitalSource { get; set; }
        public int? StepNumber { get; set; }
        public string? TypeConstruction { get; set; }
        public string? TypeManagement { get; set; }
        public string? ProjectScale { get; set; }
        public string? ProjectStandards { get; set; }
        public string? TimeOfExecution { get; set; }
        public DateTime? SettlementDate { get; set; }
        public decimal? ExecutionVolume { get; set; }
        public decimal? PlannedVolume { get; set; }
        public decimal? ContractVolume { get; set; }
        public decimal? Progress { get; set; }
        public string? ContractorName { get; set; } = string.Empty;
        public string? ContractorCode { get; set; } = string.Empty;
        public string? ConstructionOwnerCode { get; set; } = string.Empty;
        public string? ConstructionOwnerName { get; set; } = string.Empty;
        public int? CurrentStatus { get; set; }
    }
}
