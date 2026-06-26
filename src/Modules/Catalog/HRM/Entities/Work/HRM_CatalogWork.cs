using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Work;
public class HRM_CatalogWork
{
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public string? WorkCode { get; set; }
    public string? WorkName { get; set; }
    public string? CodeBranch { get; set; }
    public string? NameBranch { get; set; }
    public string? CodeObj { get; set; }
    public string? NameObj { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
    public bool? IsActive { get; set; }
    public string? CodeUnit { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
}
