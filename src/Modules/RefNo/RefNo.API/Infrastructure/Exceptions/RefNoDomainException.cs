using System;

namespace RefNo.API.Infrastructure.Exceptions;

/// <summary>
///     Exception type for app exceptions
/// </summary>
public class RefNoDomainException : Exception
{
    public RefNoDomainException()
    {
    }

    public RefNoDomainException(string message)
        : base(message)
    {
    }

    public RefNoDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
