using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogProductType
    {
        [Key]
        [Unique(nameof(CatalogProductType), nameof(ProductTypeCode), ErrorMessage = "Giá trị này đã tồn tại")]
        
        [Required(ErrorMessage = "Mã loại hàng hóa không được để trống")]
        public string ProductTypeCode { get; set; }
        public string? ProductTypeName { get; set; }
        public string? Notes { get; set; }
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public bool IsActive { get; set; }
        public bool NoAccumulationPoint { get; set; }
        public string? SignType { get; set; }
    }
}
