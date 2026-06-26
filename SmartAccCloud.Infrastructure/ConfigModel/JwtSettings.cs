using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Infrastructure.ConfigModel;

public class JwtSettings : IValidatableObject
{
    public string Key { get; set; } = string.Empty;
    public int TokenDurationInMinutes { get; set; }
    public int RefreshTokenDurationInDay { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Key))
        {
            yield return new ValidationResult(
                $"{nameof(Key)} is not configured",
                new[] { nameof(Key) });
        }
    }
}