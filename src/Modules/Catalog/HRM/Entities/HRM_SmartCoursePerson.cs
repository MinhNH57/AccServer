using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_SmartCoursePerson
{
    public Guid? Id { get; set; }
    //public int? IdAsc;
    public string? IdContents { get; set; }
    public string? CodeObj { get; set; }
    public string? NameObj { get; set; }
    public DateTime? JoinDate { get; set; }
    public bool? IsActive { get; set; }
    public string? Status { get; set; }
}
