namespace FixedAsset.API.Application.Models;

public class RelatedVoucher
{
    public Guid RefId { get; set; }
    public string RefNo { get; set; }
    public DateTime RefDate { get; set; }
    public DateTime PostedDate { get; set; }
    public int RefType { get; set; }
}