namespace SmartAccCloud.Domain.Entity.SmartFund;

public class CatalogVillage
{
    public string? CodeWards {get;set;}
    public string? NameWards {get;set;}
    public string CodeVillage {get;set;}
    public string? NameVillage {get;set;}
    public string? CodeObj {get;set;}
    public string? NameObj {get;set;}
    public DateTime? BeginDate {get;set;}
    public Guid Id {get;set;}
    public int IdAsc {get;set;}
    public int? CodeUnit {get;set;}
    public string? Notes {get;set;}
    public bool IsActive {get;set;}
    public DateTime? DateEnd {get;set;}
    public string? Represent {get;set;}
    public string? Position {get;set;}
    public string? ObjAddress {get;set;}
    public string? CitizenIDNumber {get;set;}
    public DateTime? DateOfIssue {get;set;}
    public string? PhoneNumber {get;set;}
    public string? PlaceOfIssue {get;set;}
    //public double? 2 {get;set;}
    //public double? 3 {get;set;}
    //public double? 10 {get;set;}
    public double? CommissionsWards {get;set;}
    public double? CommissionsProvince {get;set;}
    public double? CommissionsCollaborator {get;set;}
    public double? WardsPercent {get;set;}
    public double? ProvincePercent {get;set;}
    public double? CollaboratorPercent {get;set;}
    public bool IsUse {get;set;}
}