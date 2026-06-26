namespace Catalog.HRM.Entities;

public class CatalogTableCommon
{
    public string? NameTable {get;set;}
    public bool IsCommon {get;set;}
    public string? Notes {get;set;}
    public int? CodeUnit {get;set;}
    public bool IsActive {get;set;}
    public int IdAsc {get;set;}
    public Guid Id {get;set;}
}