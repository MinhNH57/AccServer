using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork;
public class HRM_CardItem
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public Guid? IdCard { get; set; }
    public string? CardItemCode { get; set; }
    public string? CardItemTitle { get; set; }
    public string? CodeObj { get; set; }
    public string? NameObj { get; set; }
    public string? CodeCustomObj { get; set; }
    public string? NameCustomObj { get; set; }
    public bool? IsDone { get; set; }
    public string? Createby { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public bool? IsCompletionRequired { get; set; }
    public Guid? ParentCardItemId { get; set; }
    public string? ParentLevel { get; set; }
}
