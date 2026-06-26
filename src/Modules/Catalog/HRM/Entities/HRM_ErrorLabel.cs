using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_ErrorLabel
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public string? HRM_ErrorLabelCode { get; set; }
    public string? ErrorLabel { get; set; }
    public string? Notes { get; set; }
    public string? CodeUnit { get; set; }
}
