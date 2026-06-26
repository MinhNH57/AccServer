using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogConstruction
    {
        public string? ProjectCode { get; set; }
        public string? ProjectName { get; set; }
        public string? GrpCode { get; set; }
        public string? GrpName { get; set; }
        [Key]
        [Unique(nameof(CatalogConstruction), nameof(ConstructionCode), ErrorMessage = "Giá trị này đã tồn tại")]
        
        [Required(ErrorMessage = "Mã dự án không được để trống")]
        public string ConstructionCode { get; set; }
        public string? ConstructionName { get; set; }
        [Precision(15,2)]
        public decimal? ValueConstruction { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PaymentTerm { get; set; }
        public string? Notes { get; set; }
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public bool IsActive { get; set; }
        public string? Status { get; set; }
        public string? Investor { get; set; }
        public string? AddressConstruction { get; set; }
        public string? Interpretation { get; set; }
        public bool? TrackingStatus { get; set; }
    }
}
