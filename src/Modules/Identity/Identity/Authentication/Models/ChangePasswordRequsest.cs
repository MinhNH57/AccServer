namespace Identity.Authentication.Models;

public class ChangePasswordRequsest
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string RepPassword { get; set; }
}

public class ResetPasswordRequest
{
    public string UserName { get; set; } = "";
    public string Token { get; set; } = "";
    public string Password { get; set; } = "";
    public string ConfirmPassword { get; set; } = "";
}

public class ForgotPasswordRequest
{
    public string Token { get; set; } = "";
    public string UserName { get; set; } = "";
}
public class ForgotPasswordVerifyCodeRequest
{
    public string Token { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Code { get; set; } = "";
}