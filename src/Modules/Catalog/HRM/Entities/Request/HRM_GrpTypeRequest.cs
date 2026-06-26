using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Request;
public class HRM_GrpTypeRequest
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public string? NameType { get; set; }
    public string? GrpTypeRequestCode { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }   
    public string? ModifyBy { get; set; }
}
