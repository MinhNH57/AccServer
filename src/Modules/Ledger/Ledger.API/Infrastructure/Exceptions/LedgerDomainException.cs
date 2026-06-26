using System;

namespace Ledger.API.Infrastructure.Exceptions;

/// <summary>
///     Exception type for app exceptions
/// </summary>
public class LedgerDomainException : Exception
{
    public LedgerDomainException()
    {
    }

    public LedgerDomainException(string message)
        : base(message)
    {
    }

    public LedgerDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
