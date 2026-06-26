namespace Identity.User.Models;

public class CreateUpdateUserRequest
{
    public string CodeUser { get; set; } = string.Empty;
    public int? CodeUnit { get; set; } = 100;
    public string NameUser { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Note { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsActive { get; set; }
    public List<RuleUserVm>? LstRules { get; set; }
}