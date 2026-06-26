using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_CatalogSkill
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public string? SkillCode { get; set; }
    public string? SkillName { get; set; }
    public string? Notes { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public bool? IsActive { get; set; }
    public string? CodeUnit { get; set; }
}
