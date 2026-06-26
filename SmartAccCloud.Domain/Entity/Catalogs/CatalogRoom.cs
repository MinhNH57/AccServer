using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogRoom
    {
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        [Key]
        [Unique(nameof(CatalogRoom), nameof(CodeRoom), ErrorMessage = "Giá trị này đã tồn tại")]
        
        [Required(ErrorMessage = "Mã phòng ban không được để trống")]
        public string CodeRoom { get; set; }
        [Required(ErrorMessage = "Tên phòng ban không được để trống")]
        public string? NameRoom { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
