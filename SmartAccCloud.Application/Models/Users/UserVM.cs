namespace SmartAccCloud.Application.Models.Users;

public class UserVm
{
    public Guid Id { get; set; }
    public int? CodeUnit { get; set; }
    public string? CodeUser { get; set; }
    public string? NameUser { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsActive { get; set; }
    public string? Note { get; set; }

}