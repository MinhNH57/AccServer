namespace Catalog.Fund.Entities;

public class SurveyExpertise
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid IdSource { get; set; }
    public bool IsStableResidence { get; set; }
    public bool GuarantorIsStableResidence { get; set; }
    public bool IsAbleToWork { get; set; }
    public bool GuarantorIsAbleToWork { get; set; }
    public bool IsStableIncome { get; set; }
    public bool GuarantorIsStableIncome { get; set; }
    public string? CodeFamilyCircumstance { get; set; }
    public string? NameFamilyCircumstance { get; set; }
    public string? GuarantorCodeFamilyCircumstance { get; set; }
    public string? GuarantorNameFamilyCircumstance { get; set; }
    public bool IsHaveDebt { get; set; }
    public bool GuarantorIsHaveDebt { get; set; }
    public string? DependentPerson { get; set; }
    public bool IsCreditDiscipline { get; set; }
    public bool IsAgree { get; set; }
    public double? Amount { get; set; }
    public string? CreditProductCode { get; set; }
    public string? CreditProductName { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? LastModified { get; set; } = DateTime.Now;
    public string? LastModifiedBy { get; set; }
}