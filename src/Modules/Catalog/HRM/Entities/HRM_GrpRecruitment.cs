using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_GrpRecruitment
{
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public string? GrpRecruitmentCode { get; set; }
    public string? GrpRecruitmentName { get; set; }
    public string? CodeManageObj { get; set; }
    public string? NameManageObj { get; set; }
    public string? NameCombinObj { get; set; }
    public string? CodeCombinObj { get; set; }
    public string? Notes { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public string? CreateBy { get; set; }
    public string? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
}
