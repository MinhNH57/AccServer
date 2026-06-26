using System.ComponentModel.DataAnnotations;

namespace Catalog.Base.Entities
{
    public class CatalogProject
    {
        [Key]
        
        [Required(ErrorMessage = "Mã dự án không được để trống")]
        public string ProjectCode { get; set; }
        public string? ProjectName { get; set; }
        public string? OwnerOfProjectCode { get; set; }
        public string? OwnerOfProjectName { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Precision(16,2)]
        public decimal? ValueProject { get; set; }
        public string? Notes { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public int? CodeUnit { get; set; } = 100;
        public bool IsActive { get; set; }
        public string? ProductActivCode { get; set; }
        public string? ProductActivName { get; set; }
        public string? GrpCode { get; set; }
        public string? GrpName { get; set; }
        public string? AddressProject { get; set; }
        public string? TypeMoney { get; set; }
        public string? NumberProject { get; set; }
        public DateTime? DateRelease { get; set; }
        public string? IssuingAgency { get; set; }
        public bool TrackingStatus { get; set; }
        public string? Procedure { get; set; }
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
