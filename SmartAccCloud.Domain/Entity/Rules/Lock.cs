namespace SmartAccCloud.Domain.Entity.Rules;

public class Lock
{
    public string? CodeMonth {get;set;}
    public string? NameMonth {get;set;}
    public string? NameUnit {get;set;}
    public bool Locks {get;set;}
    public string? LockType {get;set;}
    public string? Notes {get;set;}
    public int? CodeUnit {get;set;}
    public Guid Id {get;set;}
}