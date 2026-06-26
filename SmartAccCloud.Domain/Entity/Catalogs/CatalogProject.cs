using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogProject
    {
        [Key]
        [Unique(nameof(CatalogProject), nameof(ProjectCode), ErrorMessage = "Giá trị này đã tồn tại")]
        
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
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
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

    }
}
