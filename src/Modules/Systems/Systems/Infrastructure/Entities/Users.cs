namespace Systems.Infrastructure.Entities;

public class Users
{
    public Guid Id {get;set;}
    public int? CodeUnit {get;set;}
    public string? CodeUser {get;set;}
    public string? NameUser {get;set;}
    public string? PassUser {get;set;}
    //public string? WarehoseCode {get;set;}
    //public string? WarehoseName {get;set;}
    public bool IsAdmin {get;set;}
   // public bool IsMode {get;set;}
    public bool IsActive {get;set;}
    public string? Notes {get;set;}
    //public string? APIUrl {get;set;}
    //public string? APIUser {get;set;}
    //public string? APIPassword {get;set;}
    //public string? APITaxcode {get;set;}
    //public string? DeliveryPointCode {get;set;}
    //public string? HiloUserName {get;set;}
    public string? RefreshToken {get;set;}
    public DateTime? ExpiredTime {get;set;}
}