namespace Systems.Models.HRM;

public class RuleForRoleUpdate
{
    public string? UsageConfigurationCode { get; set; }
    public string? NameRole { get; set; }
    public string? CodeUser { get; set; }
    public string? CodeUnit { get; set; }
    public bool? IsNotification { get; set; }
    public bool? IsAssign { get; set; }
    public bool? IsWork { get; set; }
    public bool? IsManageRequest { get; set; }
    public bool? IsTimekeeping { get; set; }
    public bool? IsWhoWorking { get; set; }
    public bool? IsWeeklyWorkSchedule { get; set; }
    public bool? IsSchedule { get; set; }
    public bool? IsStaff { get; set; }
    public bool? IsAdd { get; set; }
    public bool? IsReport { get; set; }
    public bool? IsLicenseManage { get; set; }
    public bool? IsSalarySlip { get; set; }
    public bool? IsKPIManage { get; set; }
    public bool? IsEndRow { get; set; }
    public bool? IsOnlineTrainding { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? Createdate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
}
