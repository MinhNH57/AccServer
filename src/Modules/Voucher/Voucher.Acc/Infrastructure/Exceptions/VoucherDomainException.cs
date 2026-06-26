namespace Voucher.Acc.Infrastructure.Exceptions;

/// <summary>
///     Exception type for app exceptions
/// </summary>
public class VoucherDomainException : Exception
{
    public VoucherDomainException()
    {
    }

    public VoucherDomainException(string message)
        : base(message)
    {
    }

    public VoucherDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
