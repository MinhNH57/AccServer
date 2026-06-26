using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_TimekeepingPocily
{
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public string? TimekeepingPocilyCode { get; set; }
    public string? CodeObj { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? General { get; set; } = false; // Chính sách chung hay riêng
}
