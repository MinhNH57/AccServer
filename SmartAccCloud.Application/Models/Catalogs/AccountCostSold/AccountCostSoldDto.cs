using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.AccountCostSold;

public class AccountCostSoldDto
{
    [Required(ErrorMessage = "Không được bỏ trống.")]
    public string? DebitSide { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống.")]
    public string? CreditSide { get; set; }

    public string? Description { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    [NotMapped] public Guid Identifier { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public Guid Id { get; set; } = Guid.NewGuid();

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}