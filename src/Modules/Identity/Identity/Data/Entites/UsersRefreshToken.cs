namespace Identity.Data.Entites;

public class UsersRefreshToken
{
    [Key]
    public Guid Id {get;set;}
    public string UserCode {get;set;} = string.Empty;
    public string ClientType { get; set; } = string.Empty;
    public string? RefreshToken {get;set;}
    public DateTime? ExpiryDate {get;set;}
    public bool IsRevoked {get;set;}
}