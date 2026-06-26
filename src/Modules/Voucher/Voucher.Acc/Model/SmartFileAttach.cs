namespace Voucher.Acc.Model;

public class SmartFileAttach
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    public Guid? IdContents { get; set; } = Guid.NewGuid();
    public int IdAsc { get; set; }
    public string? Xem { get; set; }
    public string? Description { get; set; }
    public string? PathFile { get; set; }
    public string? FileNames { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public string? Notes { get; set; }
    public string? NumberOfVouchers { get; set; }
    //public string? Status { get; set; }
    //public DateTime Created { get; set; }
    //public string? CreatedBy { get; set; }
    //public DateTime? Modified { get; set; }
    //public string? ModifiedBy { get; set; }
}
