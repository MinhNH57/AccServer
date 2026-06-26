using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_AdvancementHistory
{
    public Guid? Id { get; set; }                      // GUID tự động
    //public int? IdAsc { get; set; }                    // Số tự tăn
    public string? AdvancementHistoryCode { get; set; }
    public string? CodeWorkInformation { get; set; }
    public string? CodeObj { get; set; }

    public DateTime? EffectiveDate { get; set; }       // Ngày có hiệu lực
    public string? DecisionType { get; set; }          // Loại quyết định
    public string? ContractOrAppendixType { get; set; }// Loại HĐ/PLHĐ
    public string? DecisionNumber { get; set; }        // Số QĐ/HĐ
    public string? JobStatus { get; set; }             // Tình trạng công việc
    public string? Province { get; set; }              // Tỉnh/Thành phố
    public string? District { get; set; }              // Quận/Huyện

    public DateTime? CreatedDate { get; set; }         // Ngày tạo
    public DateTime? ModifiedDate { get; set; }       // Ngày cập nhật

    public string? CreateBy { get; set; }
    public string? ModifyBy { get; set; }

    public string? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
}
