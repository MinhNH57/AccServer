using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_CapacityPersonal
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public string? CapacityCode { get; set; }
    public string? CapacityName { get; set; }
    public string? CodeObj { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
    public string? Attachments { get; set; }
    public bool? IsActive { get; set; }
    public string? CodeUnit { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
}
