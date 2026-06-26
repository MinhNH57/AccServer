using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities
{
    public class CatalogGroupContract
    {
        [Key]
        
        [Required(ErrorMessage = "Mã nhóm hợp đồng không được để trống")]
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
