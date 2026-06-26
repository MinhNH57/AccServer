using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Asset;
public class HRM_AssetIssue
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public DateTime? IssueDate { get; set; }
    public string? IssueCode { get; set; }
    public string? AssetName { get; set; }
    public string? AssetLocation { get; set; }
    public int? Quantity { get; set; }
    public string? StockTotal { get; set; }
    public string? DeliveryLocation { get; set; }
    public decimal? Deposit { get; set; }
    public string? Note { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? CodeUnit { get; set; }
    public string? CodeObj { get; set; }
    public bool? IsActive { get; set; }
    public string? NameBranch { get; set; }
    public string? CodeBranch { get; set; }
    public string? NameObj { get; set; }
    public string? NameRoom { get; set; }
    public string? CodeRoom { get; set; }
    public TimeSpan? IssueTime { get; set; }
    public string? RevokeForTheIssuedCode { get;set; }
    public string? NameResponsibleObject { get; set; }
    public string? Type { get; set; }
    public string? TypeName { get; set; }

}
