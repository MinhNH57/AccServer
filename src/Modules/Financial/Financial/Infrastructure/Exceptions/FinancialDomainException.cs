namespace Financial.Infrastructure.Exceptions;

/// <summary>
///     Exception type for app exceptions
/// </summary>
public class FinancialDomainException : Exception
{
    public FinancialDomainException()
    {
    }

    public FinancialDomainException(string message)
        : base(message)
    {
    }

    public FinancialDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
