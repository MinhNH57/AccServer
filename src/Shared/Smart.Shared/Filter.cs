namespace Smart.Shared;

public class Filter
{
    public int Property { get; set; }
    public Operator Operator { get; set; }
    public required string Value { get; set; }
    public DataType DataType { get; set; }
    public Operand Operand { get; set; }
}
