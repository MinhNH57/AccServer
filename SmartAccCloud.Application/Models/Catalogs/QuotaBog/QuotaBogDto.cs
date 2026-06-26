using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.QuotaBog;

public class QuotaBogDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; } = 889;

    [Required(ErrorMessage = "Không được để trống")]
    public string? Content { get; set; }

    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public double? GasolineA92 { get; set; }
    public double? GasolineA95 { get; set; }
    public double? OilDiezen { get; set; }
    public double? Gas { get; set; }
    public double? OilMazut { get; set; }
    public double? ExpenseGasolineA92 { get; set; }
    public double? ExpenseGasolineA95 { get; set; }
    public double? ExpenseOilDiezen { get; set; }
    public double? ExpenseGas { get; set; }
    public double? ExpenseOilMazut { get; set; }
    public string? Notes { get; set; }
}