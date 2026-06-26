namespace SmartAccCloud.Application.Models.RuleUnit;

public class RulesUnitUpdate
{
    public string CodeUser { get; set; }
    public List<Domain.Entity.Rules.RuleUnit>? LstRuleUnits { get; set; }
}