using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork;
public class HRM_WorkDaily
{
    public Guid Id { get; set; }
    //public int IdAsc { get; set; }
    public string? WorkCode { get; set; }
    public string? WorkName { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public bool? IsDone { get; set; }
    public bool? IsAssignedManager { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? CodeWorkSapce { get; set; }
    public string? NameWorkSapce { get; set; }
    public string? CodeUnit { get; set; }
    public string? CodeObjCustom { get; set; } // Khachs 
    public string? NameObjCustom { get; set; } // Khachs
}
