namespace AddOn.Data.Entities;

public class CatalogBankOfAccount
{
    public string? AccountNumber {get;set;}
    public string? BankName {get;set;}
    public string? BankCode {get;set;}
    public string? ObjectCode {get;set;}
    public string? ObjectName {get;set;}
    public string? Notes {get;set;}
    public string? DataType {get;set;}
    public bool IsActive {get;set;}
    public Guid Identifier {get;set;}
    public int? CodeUnit {get;set;}
    public Guid Id {get;set;}
}