namespace Notification.Data.Entites;

public class CatalogRecipientOfMessage
{
    public Guid Id {get;set;}
    public bool IsActive {get;set;}
    public string? DataType {get;set;}
    public string? UserCode {get;set;}
    public string? UserName {get;set;}
    public int? CodeUnit {get;set;}
    public int IdAsc {get;set;}
    public string? Notes {get;set;}
}