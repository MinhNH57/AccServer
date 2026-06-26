using System.ComponentModel.DataAnnotations;

namespace Catalog.Base.Entities;
public class ContractDeliverySchedule
{
    public string? CodeContract { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public bool FinishedProduct { get; set; }
    public string? ProductParent { get; set; }
    public DateTime? DeliveryTime { get; set; }
    public DateTime? DeliveryTimeShip { get; set; }
    public double Quantity { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; }
    [Key]
    public Guid Id { get; set; }  
    public Guid? IdContract { get; set; }
    public Guid? IdProduct { get; set; }
    public string? Notes { get; set; }
}
