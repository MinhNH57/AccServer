namespace AddOn.Data.Entities;

public class DataBalanceFluctuations
{
    public int CodeUnit {get;set;}
    public DateTime? RecordDate {get;set;}
    public double? Amount {get;set;}
    public double? AmountBlance {get;set;}
    public string? FullContent {get;set;}
    public string? RemittanceContent {get;set;}
    public string? BankOfAccount {get;set;}
    public string? BankOfName {get;set;}
    public string? BankOfAccountReceive {get;set;}
    public string? Notes {get;set;}
    public bool IsActive { get; set; } = false;
    public Guid Id {get;set;}
    public long Timestamp { get; set; }
}