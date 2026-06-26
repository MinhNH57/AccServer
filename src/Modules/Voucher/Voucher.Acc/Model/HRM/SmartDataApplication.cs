using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voucher.Acc.Model.HRM;
public class SmartDataApplication
{
    public Guid? Id { get; set; }
    public string? DataType { get; set; }
    public string? DataName { get; set; }
    public string? NumberOfVouchers { get; set; }
    public string? CodeUnit { get; set; }
    public string? NameUnit { get; set; }
    public string? CodeObj { get; set; }
    public string? NameObj { get; set; }
    public string? StaffCode { get; set; }
    public string? StaffName { get; set; }
    public DateTime? RecordDate { get; set; }
    public DateTime? VoucherDate { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? BeginMidShift { get; set; }
    public DateTime? EndMidShift { get; set; }
    public string? Location { get; set; }
    public double? TotalDays { get; set; }
    public double? TotalHours { get; set; }
    public string? CodeType { get; set; }
    public string? NameType { get; set; }
    public double? SalaryRate { get; set; }
    public string? ShiftCodeChanges { get; set; }
    public string? ShiftCode { get; set; }
    public string? ShiftName { get; set; }
    public bool? Monday { get; set; }
    public bool? Tuesday { get; set; }
    public bool? Wednesday { get; set; }
    public bool? Thursday { get; set; }
    public bool? Friday { get; set; }
    public bool? Saturday { get; set; }
    public bool? Sunday { get; set; }
    public double? LateStart { get; set; }
    public double? EarlyMidShift { get; set; }
    public double? LateMidShift { get; set; }
    public double? EarlyEnd { get; set; }
    public string? Reason { get; set; }
    public string? AppyFor { get; set; }
    public string? Reviewer { get; set; }
    public string? Substitute { get; set; }
    public string? RelatedPerson { get; set; }
    public int? Status { get; set; }
}
