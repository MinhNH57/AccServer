using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class ContractPaymentSchedule
{
    [MaxLength(100)]
    public string? CodeContract { get; set; }

    public Guid? IdContract { get; set; }

    [MaxLength(100)]
    public string? ProductCode { get; set; }

    [MaxLength(100)]
    public string? ProductName { get; set; }

    public bool FinishedProduct { get; set; }

    [MaxLength(150)]
    public string? ProductParent { get; set; }

    public DateTime? PaymentTime { get; set; }

    public double Quantity { get; set; }

    [MaxLength(150)]
    public string? PaymentPhase { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public decimal? RatePayment { get; set; }

    public decimal? PlannedAmount { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }

    [MaxLength(250)]
    public string? Status { get; set; }

    public decimal? ForeignCurrencyAmount { get; set; }

    public bool IsActive { get; set; }

    public int? CodeUnit { get; set; }

    public Guid Id { get; set; } = Guid.NewGuid();  

    public Guid? IdProduct { get; set; }

}
