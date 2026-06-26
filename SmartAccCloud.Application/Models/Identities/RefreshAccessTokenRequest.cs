namespace SmartAccCloud.Application.Models.Identities;

public class RefreshAccessTokenRequest
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public bool IsNew { get; set; } = true;
}