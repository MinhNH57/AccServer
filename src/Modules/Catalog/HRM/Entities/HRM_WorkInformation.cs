using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_WorkInformation
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public string? CodeObj { get; set; }
    public string? WorkInformationCode { get; set; }
    public DateTime? DayOfWork { get; set; }
    public DateTime? ContractSigningDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? FormOfWork { get; set; }
    public float? Seniority { get; set; }
    public bool IsLeaved { get; set; }
    public string? ReasonLeave { get; set; }
    public DateTime? LastDateWork { get; set; }
    public string? CodeArea { get; set; }
    public string? NameArea { get; set; }
    public string? CodeBranch { get; set; }
    public string? NameBranch { get; set; }
    public string? CodeBranchParticipation { get; set; }
    public string? NameBranchParticipation { get; set; }
    public string? CodeRoom { get; set; }
    public string? NameRoom { get; set; }
    public string? CodeRoomParticipation { get; set; }
    public string? NameRoomParticipation { get; set; }
    public string? CodePosition { get; set; }
    public string? NamePosition { get; set; }
    public string? CodePositionParticipation { get; set; }
    public string? NamePositionParticipation { get; set; }
    public string? CodeGrpObj { get; set; }
    public string? NameGrpObj { get; set; }
    public string? DirectManagement { get; set; }
    public bool IsSupremeGovernance { get; set; }
    public string? CodeUnit { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public bool? IsActive { get; set; }
    public string? Notes { get; set; }
    public float? AllowLate { get; set; }
    public float? AllowEarly { get; set; }
    public float? Salary { get; set; }
    public decimal? SalaryLevel { get; set; }
}
