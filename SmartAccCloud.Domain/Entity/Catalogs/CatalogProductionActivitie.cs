using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogProductionActivitie
    {
        [Key]
        [Unique(nameof(CatalogProductionActivitie), nameof(ProductActivCode), ErrorMessage = "Mã tồn tại")]
        [Required(ErrorMessage = "Mã không được để trống")]
        
        public string ProductActivCode { get; set; }
        public string? ProductActivName { get; set; }
        public string? Notes { get; set; }
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public bool IsActive { get; set; }
    }
}
