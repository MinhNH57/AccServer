namespace FixedAsset.API.Application.Models;

public class SelectSUFromFADecrement
{
    public Guid? RefId { get; set; }
    public Guid? FixedAssetId { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public DateTime? PostedDate { get; set; }
    public DateTime? RefDate { get; set; }
    public string RefNo { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public double? RemainingAmount { get; set; }
    public string RemainingAccount { get; set; }
    public string OrgPriceAccount { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public int? OrganizationUnitType { get; set; }
    public int RefType { get; set; }
    public string JournalMemo { get; set; }
    public Guid? RefDetailId { get; set; }
}