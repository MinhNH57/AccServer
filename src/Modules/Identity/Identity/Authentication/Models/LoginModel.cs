namespace Identity.Authentication.Models;

public class LoginModel
{
    public int CodeUnit { get; set; } = 0;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsNew { get; set; } = true;
    public string? DeviceId { get; set; }
    public string? TokenMessage { get; set; }
}