using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogRevExp
    {
        [Key]
        [Unique(nameof(CatalogRevExp), nameof(CodeRevExp), ErrorMessage = "Mã tồn tại")]
        [Required(ErrorMessage = "Mã phiếu không được để trống")]
        public string CodeRevExp { get; set; }
        public string? GrpCode { get; set; }
        public string? GrpName { get; set; }
        public string? NameRevExp { get; set; }
        [Required(ErrorMessage = "Mã loại phiếu không được để trống")]
        public string TypeRevExp { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public Guid Id { get; set; } = Guid.NewGuid();
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public bool ByObject { get; set; }
    }
}
