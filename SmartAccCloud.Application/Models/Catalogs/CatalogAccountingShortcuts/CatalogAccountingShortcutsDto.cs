using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.CatalogAccountingShortcuts;

public class CatalogAccountingShortcutsDto
{
    public string SignVoucher { get; set; }
    public string? TypeVoucher { get; set; }

    [Required(ErrorMessage = "Mã hạch toán tốc ký đã tồn tại")]
    public string AccountingShortcutsCode { get; set; }

    public string? AccountingShortcutsContents { get; set; }
    public string? DebitSide { get; set; }
    public string? CreditSide { get; set; }
    public string? Notes { get; set; }
}