namespace Catalog.HRM.Entities;

public class HRM_CatalogNation
{
    public Guid? Id { get; set; }
    public string? NationCode { get; set; }
    public string? NationName { get; set; }
    public bool? Arrange { get; set; }
    public bool? Show { get; set; }
    public bool? PositionLevel { get; set; }

    public DateTime? CreateDate { get; set; }
    public int? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public int? ModifyBy { get; set; }
    public int? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
    public string? Notes { get; set; }
}