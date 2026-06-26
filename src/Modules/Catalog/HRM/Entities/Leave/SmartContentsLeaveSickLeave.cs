using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Leave;
public class SmartContentsLeaveSickLeave
{
    public Guid IdContents { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? SalaryTime { get; set; }
    public string? SalaryRating { get; set; }
    public string? Notes { get; set; }
    public string? CodeWork { get; set; }
    public string? WorkContents { get; set; }
    public DateTime? DateIn { get; set; }
    public DateTime? DateOut { get; set; }
    public bool? IsLeaveHalfDay { get; set; }
    public Guid IdSalaryHarmony { get; set; }
    public string? NumberOfVouchers { get; set; }
    public string? CodePosition { get; set; }
    public string? WorkPosition { get; set; }
    public bool? IsIndirectly { get; set; }
    public string? CodeRoom { get; set; }
    public string? NameRoom { get; set; }
    public DateTime? DateIndirectly { get; set; }
    public bool? NoWork { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public Guid Id { get; set; }
    public string? NameUnit { get; set; }
    public string? DataType { get; set; }
    public bool? Approved { get; set; }
}
