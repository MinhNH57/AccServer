using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork;
public class HRM_ErrorsAndSolutions
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public string? Type { get; set; }
    public Guid? IdContents { get; set; }
    public string? ErrorCode { get; set; }
    public string? CodeLabel { get; set; }
    public string? ErrorName { get; set; }
    public string? Description { get; set; }
    public string? Solution { get; set; }
    public string? CodeUnit { get; set; }
    public string? CreateBy { get; set; }
    public string? CreateByName { get; set; }     
    public DateTime? CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public bool? IsNew { get; set; }
}
