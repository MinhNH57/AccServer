using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.SGas.Entities
{
    public class CatalogGroupConstruction
    {
        [Key]
        [Required(ErrorMessage = "Mã nhóm dự án không được để trống")]
        
        public string GrpCode { get; set; }
        public string? GrpName { get; set; }
        public string? Notes { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public bool IsActive { get; set; }
    }
}
