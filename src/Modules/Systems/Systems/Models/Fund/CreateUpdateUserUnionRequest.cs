using Systems.Infrastructure.Entities.SmartFund;

namespace Systems.Models.Fund;

public class CreateUpdateUserUnionRequest
{
    public string CodeUser { get; set; } = string.Empty;
    public int? CodeUnit { get; set; } = 100;
    public string NameUser { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Note { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsActive { get; set; }
    public List<RuleBaseUnion>? LstRules { get; set; }
}