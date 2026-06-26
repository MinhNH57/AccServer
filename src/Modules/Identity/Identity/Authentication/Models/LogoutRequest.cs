namespace Identity.Authentication.Models;

public class LogoutRequest
{
    public string AccessToken { get; set; } = string.Empty;
    public bool IsNew { get; set; } = true;
}