namespace Catalog.Base.Entities;
public class CatalogBidPackage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string IdObject { get; set; }
    public string? ObjCode { get; set; }
    public string? ObjName { get; set; }
    public string? ConstructionCode { get; set; }
    public string? ConstructionName { get; set; }
    public string? ContractNumber { get; set; }
    public string? ContentContract { get; set; }
    public decimal? ValueContract { get; set; }
    public decimal? ConversionLiquidationValue { get; set; }
    public decimal? BiddingPackagePrice { get; set; }
    public string? ExpenseType { get; set; }
    public string? InvestmentGoals { get; set; }
    public string? InvestmentContent { get; set; }
    public decimal? CapitalSourceProject { get; set; }
    public string? GrpCodeContract { get; set; }
    public string? GrpNameContract { get; set; }
    public string? FormOfContract { get; set; }
    public string? TimeOfExecution { get; set; }
    public string? BuyMore { get; set; }
    public string? FormOfContractorSelection { get; set; }
    public string? ContractorSelectionMethod { get; set; }
    public string? BiddingMonitoring { get; set; }
    public string? OrganizingTime { get; set; }
    public string? SpendingOnContracts { get; set; }
    public string? ContractorLegalExpenses { get; set; }
    public string? Notes { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }
    public int? Day { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public string? ProjectCode { get; set; }
    public string? ProjectName { get; set; }
}
