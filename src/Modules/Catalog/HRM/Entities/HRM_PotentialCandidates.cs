using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_PotentialCandidates
{
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public string? PotentialCandidatesCode { get; set; }
    public string? PotentialCandidatesName { get; set; }
    public string? Notes { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public string? CreateBy { get; set; }
    public string? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
}
