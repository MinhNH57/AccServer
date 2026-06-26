namespace SmartAccCloud.Domain.Entity.Rules;

public class RuleUnit
{
    public string? CodeUser { get; set; }
    public int? CodeUnitOk { get; set; }
    public int? CodeUnit { get; set; }
    public string? NameUnit { get; set; }
    public bool IsAllow { get; set; }
    public Guid Id { get; set; }
    public string? Notes { get; set; }
}