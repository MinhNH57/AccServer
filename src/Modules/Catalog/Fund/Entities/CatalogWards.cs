using System.ComponentModel.DataAnnotations;

namespace Catalog.Fund.Entities;
public class CatalogWards
{
    public string? CodeDistrict { get; set; }
    public string? NameDistrict { get; set; }
    [Key]
    public string CodeWards { get; set; }
    public string? NameWards { get; set; }
    public DateTime? BeginDate { get; set; }
    public Guid Id { get; set; }
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public string? CodeObj { get; set; }
    public string? NameObj { get; set; }
    public DateTime? DateEnd { get; set; }
    public string? Represent { get; set; }
    public string? Position { get; set; }
    public string? ObjAddress { get; set; }
    public string? CitizenIDNumber { get; set; }
    public DateTime? DateOfIssue { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PlaceOfIssue { get; set; }
    public double? WardsPercent { get; set; }
    public double? ProvincePercent { get; set; }
    public double? CollaboratorPercent { get; set; }
    public bool IsUse { get; set; }
}
