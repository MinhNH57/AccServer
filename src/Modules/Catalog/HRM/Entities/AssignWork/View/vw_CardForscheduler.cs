using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork.View;
public class vw_CardForscheduler
{
    public Guid? Id { get; set; }                       // Id của Workspace 
    public string? CodeUnit { get; set; }               // Mã đơn vị (lọc theo CodeUnit)
    public Guid? CardId { get; set; }                   // ID thẻ công việc
    public string? CardTitle { get; set; }              // Tiêu đề công việc
    public string? CardNotes { get; set; }              // Ghi chú công việc
    public DateTime? BeginDate { get; set; }            // Ngày bắt đầu
    public DateTime? EndDate { get; set; }              // Ngày kết thúc
    public bool? IsDone { get; set; }                   // Trạng thái hoàn thành
    public string? EmployeeList { get; set; }           // Danh sách nhân viên (chuỗi gộp)
    public string? statusCard { get; set; } // Dùng để xác định thuộc nhóm công việc nào , liên kết với Key ở bảng HRM_Board
}
