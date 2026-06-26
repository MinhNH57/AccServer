using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Asset;
public class HRM_HistoryOfPropertyDamage
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public string? Type { get; set; }
    public string? HistoryOfPropertyDamageCode { get; set; }
    public string? CodeAsset { get; set; }
    public string? NameAsset { get; set; }
    public DateTime? DamageDate { get; set; }
    public double? Compensation { get; set; }
    public string? CodeBranch { get; set; }
    public string? NameBranch { get; set; }
    public string? CodeObj { get; set; }
    public string? NameObj { get; set; }
    public string? DegreeOfDamage { get; set; }
    public string? Reason { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public string? Codeunit { get; set; }
    public bool? IsActive { get; set; }
    public string? CodeaAssetIssue { get; set; }
}
