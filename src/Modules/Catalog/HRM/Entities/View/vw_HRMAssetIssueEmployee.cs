using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.View;
public class vw_HRMAssetIssueEmployee
{
    // View hiển thị tài sản và nhân viên
    public Guid? Id { get; set; }
    public string? IssueCode { get; set; }
    public string? ObjCode { get; set; }
    public string? ObjName { get; set; }
    public string? NameRoom { get; set; }
    public string? CodeRoom { get; set; }
    public int? CodeUnit { get; set; }
    public string? AssetName { get; set; }
    public string? AreaCode { get; set; }
    public string? AreaName { get; set; }
    public DateTime? IssueDate { get; set; }
    public string? Position { get; set; }
    public string? CodePosition { get; set; }
    public bool? PersonStatus { get; set; }
    public bool? AssetIssueStatus { get; set; }
    public int? Quantity { get; set; }  
    public string? Note { get; set; }
    public string? Type { get; set; }
    }
