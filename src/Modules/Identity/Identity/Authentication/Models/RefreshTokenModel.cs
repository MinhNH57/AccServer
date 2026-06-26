namespace Identity.Authentication.Models;

public class RefreshTokenModel
{
    public string Content { get; set; }
    public DateTime ExpiredTime { get; set; }
}