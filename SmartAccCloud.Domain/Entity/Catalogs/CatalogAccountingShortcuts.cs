using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class CatalogAccountingShortcuts
{
    public string SignVoucher { get; set; }
    public string? TypeVoucher { get; set; }
    [Key]
    [Unique(nameof(CatalogAccountingShortcuts), nameof(AccountingShortcutsCode), ErrorMessage = "Giá trị này đã tồn tại")]
    [Required(ErrorMessage = "Mã hạch toán tốc ký đã tồn tại")]
    public string AccountingShortcutsCode { get; set; }
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
