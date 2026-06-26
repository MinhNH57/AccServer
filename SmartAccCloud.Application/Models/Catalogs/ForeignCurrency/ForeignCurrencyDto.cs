using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.ForeignCurrency;

public class ForeignCurrencyDto
{
    [Required(ErrorMessage = "Không được để trống.")]
    public string? ForeignCurrencyType { get; set; }

    [Required(ErrorMessage = "Không được để trống.")]
    public double? ExchangeRate { get; set; }

    public bool IsActive { get; set; }
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public int? CodeUnit { get; set; } = 100;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public string? Notes { get; set; }
}