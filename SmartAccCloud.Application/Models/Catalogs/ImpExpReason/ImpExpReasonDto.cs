using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.ImpExpReason;

public class ImpExpReasonDto
{
    [Key]
    [Required(ErrorMessage = "Mã lý do không được để trống")]
    public string CodeReason { get; set; }

    [Required(ErrorMessage = "Tên lý do không được để trống")]
    public string? NameReason { get; set; }

    [Required(ErrorMessage = "Kí hiệu lý do không được để trống")]
    public string? TypeReason { get; set; }

    public string? MethodOfPayments { get; set; }
    public string? Notes { get; set; }
    [NotMapped] public Guid Id { get; set; }
    public bool IsSales { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public string? DataTypeVoucher { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public string? DebitSide { get; set; }
    public string? CreditSide { get; set; }
    public string? DataTypeUse { get; set; }
}