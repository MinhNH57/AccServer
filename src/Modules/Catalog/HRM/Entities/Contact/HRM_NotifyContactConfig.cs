using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Contact;
public class HRM_NotifyContactConfig
{
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public string? CodeUnit { get; set; }
    public string? NotifyContactConfigCode { get; set; }
    public bool? IsSendToEmail { get; set; }
    public string? EmailListJson { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public bool? IsActive { get; set; }
}
