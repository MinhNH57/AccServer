using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogAccountType
    {
        [Key]
        [Unique(nameof(CatalogAccountType), nameof(AccountTypeCode), ErrorMessage = "Giá trị này đã tồn tại")]
        [Required(ErrorMessage = "Mã loại không được để trống")]
        public string AccountTypeCode { get; set; }
        [Required(ErrorMessage = "Tên loại khoản không được để trống")]
        public string AccountTypeName { get; set; }
        public string? Notes { get; set; }
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public bool IsActive { get; set; }
    }
}
