using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities
{
    public class CatalogRoom
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        [Key] 
        [Required(ErrorMessage = "Mã phòng ban không được để trống")]
        public string CodeRoom { get; set; } = string.Empty;
        [Required(ErrorMessage = "Tên phòng ban không được để trống")]
        public string? NameRoom { get; set; }  
        public string? ParentCodeRoom { get; set; }
        public bool IsParent { get; set; }
        public string? ParentNameRoom { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
