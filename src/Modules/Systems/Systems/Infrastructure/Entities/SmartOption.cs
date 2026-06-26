namespace Systems.Infrastructure.Entities;

public class SmartOption
{
    public Guid Id { get; set; }
    public int CodeUnit { get; set; }
    public string? OpType { get; set; }
    public string? Contenst { get; set; }
    public bool IsActive { get; set; }
    public string? Notes { get; set; }
    public string? ObjectUse { get; set; }
}