using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.AccountMovement;

public class AccountMovementDto
{
    [Required(ErrorMessage = "Không được bỏ trống.")]
    public string? AccountSymbol { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống.")]
    public string? DebitSide { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống.")]
    public string? CreditSide { get; set; }

    public int? Ordinal { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public Guid Id { get; set; }
}