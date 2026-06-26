using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogArea
    {
        public string? GrpCodeArea { get; set; }
        public string? GrpNameArea { get; set; }
        [Key]
        [Unique(nameof(CatalogArea), nameof(CodeArea), ErrorMessage = "Mã đã tồn tại")]
        [Required(ErrorMessage = "Mã khu vực không được để trống")]
        public string CodeArea { get; set; }
        public string? NameArea { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
