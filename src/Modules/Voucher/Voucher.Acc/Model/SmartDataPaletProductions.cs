using Voucher.Acc.Model.Contracts;

namespace Voucher.Acc.Model;

public class SmartDataPaletProductions: IBaseEntity
{
    public Guid Id { get; set; }
    //public int IdAsc { get; set; }
    public string? MachineId { get; set; }
    public string? MachineName { get; set; }
    public string? ShiftCode { get; set; }
    public string? ShiftName { get; set; }
    public DateTime? RecodeDate { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? BatchNo { get; set; }
    public string? PaletCode { get; set; }
    public string? PaletName { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public double? WeightPalet { get; set; }
    public double? WeightPackaging { get; set; }
    public double? NumberPackaging { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; }
    public DateTime CreateDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}