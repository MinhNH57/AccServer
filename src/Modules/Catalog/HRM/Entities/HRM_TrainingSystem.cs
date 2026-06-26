namespace Catalog.HRM.Entities;

public class HRM_TrainingSystem
{
    public Guid? Id { get; set; }
    public string? TrainingSystemCode { get; set; }
    public string? TrainingSystemName { get; set; }
    public double? Duration { get; set; }
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