using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_SalaryLevel
{
    public Guid? Id { get; set; }                   // GUID tự sinh
    public string? SalaryLevelCode { get; set; }    // Mã bậc lương
    public string? SalaryLevelName { get; set; }    // Tên bậc lương
    public string? CreateBy { get; set; }           // Người tạo
    public DateTime? CreateDate { get; set; }       // Ngày tạo
    public string? ModifyBy { get; set; }           // Người sửa
    public DateTime? ModifyDate { get; set; }      // Ngày sửa
    public bool? IsActive { get; set; }             // Còn hoạt động
    public string? CodeUnit { get; set; }
}
