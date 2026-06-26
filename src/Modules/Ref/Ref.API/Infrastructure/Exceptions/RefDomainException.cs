using System;

namespace Ref.API.Infrastructure.Exceptions;

/// <summary>
///     Exception type for app exceptions
/// </summary>
public class RefDomainException : Exception
{
    public RefDomainException()
    {
    }

    public RefDomainException(string message)
        : base(message)
    {
    }

    public RefDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
