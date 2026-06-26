namespace BuildingBlocks.Response;

public sealed record Error(string Code, string Description, List<ErrorDetail>? Details = null)
{
    public static readonly Error? None = null;
    public static List<ErrorDetail>? Detail = null;
}