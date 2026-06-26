using Salary.Model.Contracts;

namespace Salary.Model;
public class SalaryTimeSheetDetail : IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid IdSalaryHead { get; set; }
    public int? CodeUnit { get; set; } 
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public string ObjectCode { get; set; } = string.Empty;
    public string ObjectName { get; set; } = string.Empty;
    public string? CodeRoom { get; set; }
    public string? NameRoom { get; set; }
    public string? WorkExp { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal GasStaffSalary { get; set; }
    public double BasicWorkingDay { get; set; }
    public double ActualWorkingDay { get; set; }
    public double NumberOfSundays { get; set; }
    public double NumberOfNonSundays { get; set; }
    public double OvertimeHours { get; set; }
    public decimal TimeMoney { get; set; }
    public decimal ProductionMoney { get; set; }
    public decimal ProductionBonusPercentage { get; set; }
    public decimal MoneyOvertime { get; set; }
    public decimal MoneyDiligence { get; set; }
    public decimal MoneyGas { get; set; }
    public decimal MoneySupOvertime { get; set; }
    public decimal MoneyPhone { get; set; }
    public decimal MoneyHolidaySunday { get; set; }
    public decimal MoneySeniority { get; set; }
    public decimal MoneyEat { get; set; }
    public decimal MoneyChild { get; set; }
    public decimal MoneyAdditionsToSalary { get; set; }
    public decimal MoneyBonusNewStaff { get; set; }
    public decimal MoneySup20PercentageStage { get; set; }
    public decimal MoneyManageWorkshop { get; set; }
    public decimal MoneyBonusResponsibility { get; set; }
    public decimal SalaryProduction { get; set; }
    public decimal SalaryTime { get; set; }
    public decimal CostUnionFees { get; set; }
    public decimal CostHealth { get; set; }
    public decimal CostAdvancesOther { get; set; }
    public decimal CostEat { get; set; }
    public decimal CostDiligence { get; set; }
    public decimal CostTotal { get; set; }
    public decimal RealProductionSalary { get; set; }
    public decimal RealProductionTime { get; set; }
    public decimal TotalSalaryPaid { get; set; }
    public DateTime? DatePaid { get; set; }
    public decimal CostQuangSon { get; set; }
    public decimal CostBacBinh { get; set; }
    public decimal CostHoaSon { get; set; }
    public decimal CostCaoPhong { get; set; }

}
