namespace Catalog.HRM.Entities;

public class HRM_catalogPosition
{
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public string? PositionCode { get; set; }
    public string? PositionName { get; set; }
    public int? CodeUnit { get; set; }
    public bool? Arrange { get; set; }
    public bool? Show { get; set; }
    public bool? PositionLevel { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? Notes { get; set; }
}