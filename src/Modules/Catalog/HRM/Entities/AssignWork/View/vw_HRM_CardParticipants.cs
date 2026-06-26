using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork.View;
public class vw_HRM_CardParticipants
{
    public Guid? CardId { get; set; }
    public string? CardTitle { get; set; }
    public string? CardNotes { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? CardStatus { get; set; }
    public bool? IsDone { get; set; }

    // Thống kê tiến độ
    public int? DaysTaken { get; set; }      // Số ngày thực hiện
    public int? EarlyDays { get; set; }       // Số ngày hoàn thành sớm
    public int? LateDays { get; set; }        // Số ngày trễ deadline

    // Workspace
    public Guid? Id { get; set; }
    public string? WorkSpaceCode { get; set; }
    public string? WorkSpaceName { get; set; }
    public string? WsCodeManageObj { get; set; }
    public string? WsNameManageObj { get; set; }
    public string? WsCodeRoom { get; set; }   // Mã phòng ban
    public string? WsNameRoom { get; set; }

    // Nhân viên
    public string? EmployeeCode { get; set; }
    public string? EmployeeName { get; set; }
    public string? EmployeeEmail { get; set; }

    // Nhóm 
    public Guid? BoardId { get; set; }
    public string? BoardTitle { get; set; }

    public string? CodeUnit { get; set; }
    public DateTime? ModifyDate { get; set; }
}
