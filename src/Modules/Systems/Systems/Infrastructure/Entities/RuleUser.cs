namespace Systems.Infrastructure.Entities;

public class RuleUser
{
    [Key]
    public Guid Id { get; set; }
    public int CodeUnit { get; set; }
    public string KeyFuntion { get; set; }
    public string NameFuntion { get; set; }
    public string CodeUser { get; set; }
    public bool AllowInsert { get; set; }
    public bool AllowEdit { get; set; }
    public bool AllowDelete { get; set; }
    public bool AllowView { get; set; }
    public bool AllowPrint { get; set; }
    public string Notes { get; set; }
}