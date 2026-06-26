namespace BuildingBlocks.Logging;

public class LogOptions
{
    public string Level { get; set; }
    public string LogTemplate { get; set; }
    public FileOption FileOption { get; set; }
    public SeqOption SeqOption { get; set; }
}