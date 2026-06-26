using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogImpExpReason
    {
        [Key]
        [Unique(nameof(CatalogImpExpReason), nameof(CodeReason), ErrorMessage = "Giá trị này đã tồn tại")]

        [Required(ErrorMessage = "Mã lý do không được để trống")]
        public string CodeReason { get; set; }
        public string? NameReason { get; set; }
        public string? TypeReason { get; set; }
        public string? MethodOfPayments { get; set; }
        public string? Notes { get; set; }
        public Guid Id { get; set; }
        public bool IsSales { get; set; }
        public string? DebitSide { get; set; }
        public string? CreditSide { get; set; }
        public bool IsActive { get; set; }
        public int? CodeUnit { get; set; } = 100;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public string? DataTypeUse { get; set; }
        public string? DataTypeVoucher { get; set; }
    }
}
