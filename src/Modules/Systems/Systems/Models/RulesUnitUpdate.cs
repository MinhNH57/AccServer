using Systems.Infrastructure.Entities;

namespace Systems.Models;

public class RulesUnitUpdate
{
    public string CodeUser { get; set; }
    public List<RuleUnit>? LstRuleUnits { get; set; }
}