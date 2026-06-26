namespace FixedAsset.Domain.Exceptions;

/// <summary>
/// Exception type for domain exceptions
/// </summary>
public class FixedAssetDomainException : Exception
{
    public FixedAssetDomainException()
    { }

    public FixedAssetDomainException(string message)
        : base(message)
    { }

    public FixedAssetDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
