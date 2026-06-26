using System.ComponentModel.DataAnnotations;

namespace Voucher.Acc.Model;

public class SmartConclusionQualityControl
{
    public Guid IdContents { get; set; }
    [Key]
    public int IdAsc { get; set; }

    public string DataType { get; set; } = string.Empty;
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public bool MeetsWarehouseImportStandards { get; set; }
    public bool FailedRejectReturnPending { get; set; }
    public bool TemporaryWarehousePendingInspection { get; set; }
    public string? ResultOk { get; set; }
    public string? Notes { get; set; }
    public bool SaveTemplate { get; set; }
    public double? RecheckTime { get; set; }
}