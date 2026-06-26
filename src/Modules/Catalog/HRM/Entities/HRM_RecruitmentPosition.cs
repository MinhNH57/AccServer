using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_RecruitmentPosition
{
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public string? RecruitmentPositionCode { get; set; }
    public string? RecruitmentPositionName { get; set; }
    public string? GrpRecruitmentPosition { get; set; }
    public int? NumberOfCVs { get; set; }
    public int? NumberOfInterviews { get; set; }
    public int? NumberOfNewStaff { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
    public string? CodeWorkForm { get; set; }
    public string? NameWorkForm { get; set; }
    public string? CodeMajor { get; set; }
    public string? NameMajor { get; set; }
    public string? Experience { get; set; }
    public string? AgeRequirement { get; set; }
    public string? GenderRequirement { get; set; }
    public int? NumberOfRecruits { get; set; }
    public double? ProbationPeriod { get; set; }
    public DateTime? ApplicationDeadline { get; set; }
    public string? CodePosition { get; set; }
    public string? NamePosition { get; set; }
    public string? CodeWorkSpace { get; set; }
    public string? NameWorkSpace { get; set; }
    public double? MinimumSalary { get; set; }
    public double? MaximumSalary { get; set; }
    public bool? SalaryNegotiable { get; set; }
    public string? Dercription { get; set; }
    public string? JobRequirements { get; set; }
    public string? Interest { get; set; }
    public string? NameContactPerson { get; set; }
    public string? EmailContactPerson { get; set; }
    public string? PhoneContactPerson { get; set; }
    public string? AddressContactPerson { get; set; }
    public string? CodeProcedure { get; set; }
    public string? NameProcedure { get; set; }
    public string? CodeEvaluate { get; set; }
    public string? NameEvaluate { get; set; }
    public string? CodeQuestion { get; set; }
    public string? NameQuestion { get; set; }
    public string? CodeInterviewQuestions { get; set; }
    public string? NameInterviewQuestions { get; set; }
    public string? EmailToReceiveFeedback { get; set; }
    public string? CodeRecruitmentGrp { get; set; }
    public string? NameRecruitmentGrp { get; set; }
}
