namespace Systems.Models.LockRequest;

public class LockRequestStore
{
    public string Parameter { get; set; } = "LookUnit";
    public string UserCode { get; set; } = string.Empty;
    public int CodeUnit { get; set; } = 0;
    public string YearTxt { get; set; } = string.Empty;
    public string? TypeLock { get; set; } = string.Empty;
}