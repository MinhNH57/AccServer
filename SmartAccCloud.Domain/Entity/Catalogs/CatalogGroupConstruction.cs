using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogGroupConstruction
    {
        [Key]
        [Unique(nameof(CatalogGroupConstruction), nameof(GrpCode), ErrorMessage = "Mã nhóm dự án đã tồn tại")]
        [Required(ErrorMessage = "Mã nhóm dự án không được để trống")]
        
        public string GrpCode { get; set; }
        public string? GrpName { get; set; }
        public string? Notes { get; set; }
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public bool IsActive { get; set; }
    }
}
