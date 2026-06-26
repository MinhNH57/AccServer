using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork.View;
public class vw_HRM_WorkSpaceCardSummary
{
    public Guid? Id { get; set; }                 
    public string? WorkSpaceCode { get; set; } = ""; 
    public string? WorkSpaceName { get; set; } = ""; 
    public string? WorkSpaceDescription { get; set; } = ""; // Mô tả Workspace
    public bool? IsWorkSpaceIsActive { get; set; }    // Trạng thái Workspace
    public string? CodeUnit { get; set; } = "";       // Mã đơn vị
    public string? WorkSpaceCreateBy { get; set; } = ""; // Người tạo
    public DateTime? WorkSpaceCreateDate { get; set; }  // Ngày tạo
    public string? WsNameManageObj { get; set; } = ""; // Tên người quản lý
    public string? WsNameRoom { get; set; } = "";      // Tên phòng ban
    public string? WsCodeManageObj { get; set; } = "";
    public int? TotalCards { get; set; }              // Tổng số Card
    public int? CompletedCards { get; set; }          // Số Card đã hoàn thành
    public int? PendingCards { get; set; }            // Số Card chưa hoàn thành
    public decimal? CompletionPercentage { get; set; } // % hoàn thành
    public DateTime UpdateAt { get; set; }  
    public string? ProjectStatus { get; set; } = "";
}
