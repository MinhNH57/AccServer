namespace SmartAccCloud.Application.Models.Identities;

public class RefreshTokenModel
{
    public string Content { get; set; }
    public DateTime ExpiredTime { get; set; }
}