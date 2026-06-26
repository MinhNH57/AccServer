using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities
{
    public class CatalogAccountType
    {
        [Key]
        [Required(ErrorMessage = "Mã loại không được để trống")]
        public string AccountTypeCode { get; set; } = null!;
        [Required(ErrorMessage = "Tên loại khoản không được để trống")]
        public string AccountTypeName { get; set; }
        public string? Notes { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public bool IsActive { get; set; }
    }
}
