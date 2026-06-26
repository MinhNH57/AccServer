using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_RequestUser
{
    public Guid? Id { get; set; }
    public Guid? IdContents { get; set; }
    public string? CodeUnit { get; set; }
    public string? RequestCode { get; set; }
    public string? Type { get; set; }
    public string? Contents { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? RecordDate { get; set; }
    public string? Reason { get; set; }
    public string? Approver { get; set; }
    public string? ApproverName { get; set; }
    public string? CodeRoom { get; set; }
    public string? NameRoom { get; set; }
    public string? CodeUserRequest { get; set; }
    public string? NameUserRequest { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public bool? Approved { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public string? CodeObj { get; set; }
    public string? NameObj { get; set; }
}
