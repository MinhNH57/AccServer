namespace Identity.Data.Entites;

public class CatalogObject
{
    public Guid Id {get;set;} 
    public int? CodeUnit {get;set;}
    public string ObjCode {get;set;}
    public string? ObjName {get;set;}
    public string? CitizenIDNumber { get; set; } 
    public bool? IsStaff {get;set;}
    public bool? IsActive {get;set;}
}