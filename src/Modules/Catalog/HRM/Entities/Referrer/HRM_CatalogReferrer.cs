using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Referrer;
public class HRM_CatalogReferrer
{
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public string? ReferrerCode { get; set; }
    public string? ObjName { get; set; }
    public string? CodeCandidate { get; set; }
    public string? NameCandidate { get; set; }
    public string? CodePosition { get; set; }
    public string? NamePosition { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public bool? IsActive { get; set; }
    public string? CodeUnit { get; set; }
}
