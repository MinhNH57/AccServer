using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.SGas.Entities
{
    public class CatalogGroupObj
    {
        [Key]
        [Required(ErrorMessage = "Mã nhóm đơn vị không được để trống")]
        public string CodeGroupObj { get; set; }
        [Required(ErrorMessage = "Tên nhóm đơn vị không được để trống")]
        public string NameGroupObj { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public bool IsAutogen { get; set; }
        public bool AllowExpContract { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
    }
}
