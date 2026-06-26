using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities
{
    public class CatalogGroupArea
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        [Key]
        [Required(ErrorMessage = "Mã nhóm khu vực không được để trống")]
        public string CodeGroupArea { get; set; } = null!;
        [Required(ErrorMessage = "Tên nhóm khu vực không được để trống")]
        public string? NameGroupArea { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
