namespace SmartAccCloud.Domain.Entity.SmartFund;

public class CatalogFamilyRelationship
{
    public string CodeFamilyRelationship {get;set;}
    public string? NameFamilyRelationship {get;set;}
    public Guid Id {get;set;}
    public int IdAsc {get;set;}
    public int? CodeUnit {get;set;}
    public string? Notes {get;set;}
    public bool IsActive {get;set;}
}