using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_RecruitmentProcess
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public string? RecruitmentProcessCode { get; set; }
    public string? RecruitmentProcessName { get; set; }
    public string? Stages { get; set; }
    public string? CodeUnit { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? CreateBy { get; set; }
    public string? ModifyBy { get; set; }
    public bool? IsActive { get; set; }
}
