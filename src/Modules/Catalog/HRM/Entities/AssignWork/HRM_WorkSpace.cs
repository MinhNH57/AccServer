using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork;
public class HRM_WorkSpace
{
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public string? WorkSpaceCode { get; set; }
    public string? Name { get; set; }
    public string? Descrition { get; set; }
    public string? CodeObjManage { get; set; }
    public string? NameObjManage { get; set; }
    public string? CodeRoom { get; set; }
    public string? NameRoom { get; set; }
    public bool? IsPrivate { get; set; }
    public bool? IsActive { get; set; }
    public string? CodeUnit { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public string? CodeCustom { get; set; }
    public string? NameCustom { get; set; }
    public string? CooperationDepartment { get; set; }  
}
