using System.ComponentModel.DataAnnotations;

namespace Catalog.SGas.Entities;

public class CatalogUnit
{
    public Guid Id {get;set;} = Guid.NewGuid();
    public int IdUnitOk {get;set;}
    [Key]
    public int CodeUnit {get;set;}
    public string? NameUnit {get;set;}
    public string? Address {get;set;}
    public bool IsActive {get;set;}
    //public string? Notes {get;set;}
    //public bool VAT {get;set;}
    public string? PositionDir {get;set;}
    public string? DirectorName {get;set;}
    public string? Taxcode {get;set;}
    //public bool RegisterSyncInv {get;set;}
    public bool ByBatchNo {get;set;}
}