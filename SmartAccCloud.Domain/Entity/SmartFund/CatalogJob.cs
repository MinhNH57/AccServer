namespace SmartAccCloud.Domain.Entity.SmartFund;

public class CatalogJob
{
    public string CodeJob {get;set;}
    public string? NameJob {get;set;}
    public string? Notes {get;set;}
    public bool IsActive {get;set;}
    public Guid Identifier {get;set;}
    public int? CodeUnit {get;set;}
    public Guid Id {get;set;}
    public int IdAsc {get;set;}
    public DateTime? Created {get;set;}
    public string? CreatedBy {get;set;}
    public DateTime? LastModified {get;set;}
    public string? LastModifiedBy {get;set;}
    public bool IsUse {get;set;}
}