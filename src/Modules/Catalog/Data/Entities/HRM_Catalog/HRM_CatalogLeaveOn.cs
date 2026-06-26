using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.HRM_Catalog;
public class HRM_CatalogLeaveOn
{
    public Guid? Id { get; set; }               // GUID tự tăng
    //public int? IdAsc { get; set; }
    public string? LeaveOnCode { get; set; }    // Mã loại nghỉ
    public string? LeaveOnName { get; set; }    // Tên loại nghỉ
    public int? TotalLeave { get; set; }       // Tổng số ngày nghỉ
    public int? Leaved { get; set; }           // Đã nghỉ
    public int? Remaining { get; set; }        // Còn lại
    public bool? IsActive { get; set; }         // Còn sử dụng hay không
    public string? CreateBy { get; set; }       // Người tạo
    public DateTime? CreateDate { get; set; }  // Ngày tạo
    public string? ModifyBy { get; set; }       // Người sửa
    public DateTime? ModifyDate { get; set; }  // Ngày sửa
    public string? CodeUnit { get; set; }       // Mã đơn vị
    public string? CodeObj { get; set; }        // Mã đối tượng
    public int Moth { get; set; }                // Tháng
}
