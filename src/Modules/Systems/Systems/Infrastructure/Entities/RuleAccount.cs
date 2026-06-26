namespace Systems.Infrastructure.Entities;

public class RuleAccount
{
    public Guid Id {get;set;}
    public int? CodeUnit {get;set;}
    public int? AccountLevel {get;set;}
    public string? AccountSymbol {get;set;}
    public string? AccountName {get;set;}
    public string? CodeUser {get;set;}
    public bool IsAllow {get;set;}
    public string? Notes {get;set;}
}