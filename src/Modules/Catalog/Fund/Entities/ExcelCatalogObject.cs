using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Fund.Entities;

public class ExcelCatalogObject
{
    public Guid Id { get; set; }
    //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    //public int IdAsc { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public string? GrpCode { get; set; }
    public string? GrpName { get; set; }
    //public string? CodeRoom {get;set;}
    //public string? NameRoom {get;set;}
    //public string? GrpAreaCode {get;set;}
    //public string? GrpAreaName {get;set;}
    //public string? AreaCode {get;set;}
    //public string? AreaName {get;set;}
    public string? CodeManager { get; set; }
    public string? NameManager { get; set; }
    public string ObjCode { get; set; }
    public string? ObjName { get; set; }
    //public double? DiscountRate {get;set;}
    //public string? Position {get;set;}
    //public string? AccPosition {get;set;}
    //public string? ObjAddress {get;set;}
    //public string? DirectorName {get;set;}
    //public string? AccName {get;set;}
    //public string? TaxCode {get;set;}
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? AccountNumber { get; set; }
    public string? BankName { get; set; }
    //public decimal? DebitBalance {get;set;}
    //public decimal? DebitLetterOfGuarantee {get;set;}
    //public decimal? DebitHeadShops {get;set;}
    //public decimal? DebitMortgage {get;set;}
    //public int? DayNumberPayment {get;set;}
    //public string? BusinessRegistrationNumber {get;set;}
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public bool IsStore { get; set; }
    public string? PermanentAddress { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? DOB { get; set; }
    public string? ObjSex { get; set; }
    public string? ObjJob { get; set; }
    public double? Income { get; set; }
    //public DateTime? BeginDate {get;set;}
    //public DateTime? EndDate {get;set;}
    //public int? CitizenID {get;set;}
    [Column(TypeName = "datetime")]
    public DateTime? RangeDate { get; set; }
    public string? GrantedBy { get; set; }
    //public string? MyObjName {get;set;}
    //public DateTime? MyDOB {get;set;}
    //public DateTime? MyRangeDate {get;set;}
    //public int? MyCitizenID {get;set;}
    //public string? MyGrantedBy {get;set;}
    //public string? MyObjAddress {get;set;}
    //public string? MyRelationship {get;set;}
    //public string? MyPhoneNumber {get;set;}
    //public string? CodeOther {get;set;}
    //public string? CodeUnitManager {get;set;}
    //public string? NameUnitManager {get;set;}
    //public string? ContractNumber {get;set;}
    //public bool VAT {get;set;}
    //public string? Buyer {get;set;}
    //public DateTime? BirthDate {get;set;}
    //public double? AccumulatedPoints {get;set;}
    //public string? LevelDiscount {get;set;}
    public string? CitizenIDNumber { get; set; }
    public string? DataType { get; set; } = "CataObj";
    public string? NumberImport { get; set; }
    public bool IsCreated { get; set; }
    public bool? IsEmergency { get; set; }
    public DateTime CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public string? TypeData { get; set; } = "CataObj";
    //Dư nợ vay tại tổ chức
    public decimal RestMoneyDebt { get; set; }
    public decimal LoanAmount { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? DebitDate { get; set; }
    public decimal BadDebtBalance { get; set; }
    public decimal OutstandingDebtNeedsAttention { get; set; }
    public string? CreditProductCode { get; set; }
    public string? CreditProductName { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? DisbursementDate { get; set; }
    public string? PurposeCode { get; set; }
    public string? PurposeName { get; set; }
    public decimal? CreateJobs { get; set; }
    public decimal? WorkYear { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }
    public string? DisbursementFormCode { get; set; }
    public string? GuarantorNameJob { get; set; }
    public string? AddressObject { get; set; }
    public string? CodeWards { get; set; }
    public string? NameWards { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public int? ContractValue { get; set; }
    public string? DisbursementFormName { get; set; }
    public bool ExistsCode { get; set; }
    public bool ExistsIDNumber { get; set; }
    public bool? IsInsurance { get; set; }
    public bool IsCreateSubmiss { get; set; }
    public string? FundingSourceCode { get; set; }
    public string? FundingSourceName { get; set; }
    public Guid? IdSubmission { get; set; }
    public bool Proposal { get; set; }
    public bool Register { get; set; }
    public string? Submission { get; set; }
    public string? PurposeCredit { get; set; }
    public string? MaritalStatus { get; set; }
    //public string? NoSubmission {get;set;}
    public int? CreditPeriod { get; set; }
    public int? MonthPeriod  { get; set; }
    //public double? InterestMonth {get;set;}
}