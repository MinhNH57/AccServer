namespace BuildingBlocks.Logging;

public class FileOption
{
    public bool Enabled { get; set; }
    public string Path { get; set; } = string.Empty;
    public string Interval { get; set; } = string.Empty;
    public int RetainedFileCountLimit { get; set; } = 7;
}