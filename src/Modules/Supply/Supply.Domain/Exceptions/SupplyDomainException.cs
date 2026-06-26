namespace Supply.Domain.Exceptions;

/// <summary>
/// Exception type for domain exceptions
/// </summary>
public class SupplyDomainException : Exception
{
    public SupplyDomainException()
    { }

    public SupplyDomainException(string message)
        : base(message)
    { }

    public SupplyDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
