using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Contact;
public class HRM_NotifyContentContactConfig
{
    public Guid? Id { get; set; }
    public Guid? IdContent { get; set; }
    public string? CodeNotifyContactConfig { get; set; }
    public string? CodeGrpContact { get; set; }
    public string? NameGrpContact { get; set; }
    public int? AdvanceNotice { get; set; }
    public bool? IsActive { get; set; }
}
