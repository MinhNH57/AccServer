using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.MethodOfPayments;

public class MethodOfPaymentsDto
{
    [Required(ErrorMessage = "Mã phương thức không được để trống")]
    public string MethodOfPaymentsCode { get; set; }

    [Required(ErrorMessage = "Tên phương thức không được để trống")]
    public string? MethodOfPaymentsName { get; set; }

    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? TypePayments { get; set; }
    public string? Notes { get; set; }
    [NotMapped] public Guid Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
}