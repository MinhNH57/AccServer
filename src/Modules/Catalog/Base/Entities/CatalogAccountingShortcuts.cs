using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class CatalogAccountingShortcuts
{
    public string? TypeVoucher { get; set; }
    public string? VoucherName { get; set; }
    [Key]
    [Required(ErrorMessage = "Mã hạch toán tốc ký đã tồn tại")]
    public string AccountingShortcutsCode { get; set; } = null!;
    public string? AccountingShortcutsContents { get; set; }
    public string? DebitSide { get; set; }
    public string? CreditSide { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
