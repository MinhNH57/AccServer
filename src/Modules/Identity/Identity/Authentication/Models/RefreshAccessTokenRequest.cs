namespace Identity.Authentication.Models;

public class RefreshAccessTokenRequest
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int CodeUnit { get; set; } = 0;
    public bool IsNew { get; set; } = true;
}