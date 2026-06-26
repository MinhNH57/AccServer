namespace Catalog.HRM.Entities;

public class HRM_CatalogDegree
{
    public Guid? Id { get; set; }
    public string? DegreeCode { get; set; }
    public string? DegreeName { get; set; }
    public bool? Arrange { get; set; }
    public bool? Show { get; set; }
    public bool? PositionLevel { get; set; }
    public string? Notes { get; set; }
    public DateTime? CreateDate { get; set; }
    public int? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public int? ModifyBy { get; set; }
    public int? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
}