namespace AuditTrait.Models;

public class Logg1
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? NameTable { get; set; }

    public Datalog DataObj { get; set; }
    public string? DataLog { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
}