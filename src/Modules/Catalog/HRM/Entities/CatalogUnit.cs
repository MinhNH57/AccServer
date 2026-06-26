using System.ComponentModel.DataAnnotations;

namespace Catalog.HRM.Entities;

public class CatalogUnit
{
    public Guid Id {get;set;}
    public int IdUnitOk {get;set;}
    [Key]
    public int CodeUnit {get;set;}
    public string? NameUnit {get;set;}
    public string? Address {get;set;}
    public bool IsActive {get;set;}
    public string? PositionDir {get;set;}
    public string? DirectorName {get;set;}
    public string? Taxcode {get;set;}
    //public bool RegisterSyncInv {get;set;}
    public bool ByBatchNo {get;set;}
    public string? ConutryName { get; set; }   // NVARCHAR(50)
    public string? CityName { get; set; }      // NVARCHAR(50)
    public string? WardName { get; set; }      // NVARCHAR(50)
    public DateTime? DateOfEstablishment { get; set; }  // DATE (nullable nếu có thể trống)
    public int? Scale { get; set; }            // INT
    public decimal? CharterCapital { get; set; } // DECIMAL(18,2)
    public string? BankName { get; set; }      // NVARCHAR(200)
    public string? STK { get; set; }           // VARCHAR(50)
    public string? Email { get; set; }         // NVARCHAR(100)
    public string? Notes { get; set; }
    public string? MST { get; set; }
}