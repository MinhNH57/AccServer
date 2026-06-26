using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogGroupArea
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        [Key]
        [Unique(nameof(CatalogGroupArea), nameof(CodeGroupArea), ErrorMessage = "Mã đã tồn tại")]
        [Required(ErrorMessage = "Mã nhóm khu vực không được để trống")]
        public string CodeGroupArea { get; set; }
        [Required(ErrorMessage = "Tên nhóm khu vực không được để trống")]
        public string? NameGroupArea { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
