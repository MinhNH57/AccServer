namespace SmartAccCloud.Application.Models.Identities;

public class LoginModel
{
    public int CodeUnit { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsNew { get; set; } = true;
}