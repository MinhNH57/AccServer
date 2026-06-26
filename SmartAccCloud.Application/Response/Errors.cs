namespace SmartAccCloud.Application.Response;

public sealed record Error(string Code, string Description)
{
    public static readonly Error? None = null;
}