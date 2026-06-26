using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_CatalogCandidate
{
    public Guid Id { get; set; }
    //public int? IdAsc;
    public string? CandidateCode { get; set; }
    public string? NameObj { get; set; }
    public string? CodeObj { get; set; }
    public string? Email{ get; set; }
    public string? AddressObj { get; set; }
    public string? Numberphone { get; set; }
    public string? Gender { get; set; }
    public string? CodeRecruitmentSourcre { get; set; }
    public string? NameRecruitmentSourcre { get; set; }
    public string? CodeRecruimentCampaign { get; set; }
    public string? NameRecruimentCampaign { get; set; }
    public bool? IsLimitedApplicants { get; set; }
    public bool? IsCoincidence { get; set; }
    public string? CreateBy { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
}
