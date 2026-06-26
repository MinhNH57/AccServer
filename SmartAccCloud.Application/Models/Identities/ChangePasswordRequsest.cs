namespace SmartAccCloud.Application.Models.Identities;

public class ChangePasswordRequsest
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string RepPassword { get; set; }
}