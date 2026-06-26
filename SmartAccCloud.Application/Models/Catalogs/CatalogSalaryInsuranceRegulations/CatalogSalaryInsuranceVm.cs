using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.CatalogSalaryInsuranceRegulations;
public class CatalogSalaryInsuranceVm
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime ApplyFromDate { get; set; }
    public DateTime ApplyToDate { get; set; }
    public decimal? BaseSalary { get; set; }
    public decimal? MaxSalaryForInsurance { get; set; }
    public decimal? PersonalDeduction { get; set; }
    public decimal? DependentDeduction { get; set; }
}
