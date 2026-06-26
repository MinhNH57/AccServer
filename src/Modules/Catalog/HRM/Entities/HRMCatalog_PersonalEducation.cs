using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRMCatalog_PersonalEducation
{
    public Guid? Id { get; set; }                       // Khóa chính
    //public int? IdAsc { get; set; }
    public string? LevelEducationCode { get; set; }     // Mã trình độ
    public string? MajorId { get; set; }                // Mã chuyên ngành
    public string? MajorName { get; set; }
    public string? TrainingPlace { get; set; }          // Nơi đào tạo
    public DateTime? GrantedDate { get; set; }         // Ngày cấp
    public string? Note { get; set; }                   // Ghi chú
    public string? CodeObj { get; set; }                // Mã đối tượng (nhân viên)
    public DateTime? CreatedDate { get; set; }          // Ngày tạo
    public string? CreatedBy { get; set; }              // Người tạo
    public DateTime? ModifiedDate { get; set; }        // Ngày sửa
    public string? ModifiedBy { get; set; }
}
