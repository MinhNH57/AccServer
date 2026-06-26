using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Jwt;

public class JwtSettings : IValidatableObject
{
    public string Key { get; set; } = string.Empty;
    public string Authority { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
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