namespace FixedAsset.API.Application.Validations;

public class UpdateFixedAssetIdentifiedCommandValidator : AbstractValidator<IdentifiedCommand<UpdateFixedAssetCommand, FAIncrementUpdateResponse>>
{
    public UpdateFixedAssetIdentifiedCommandValidator(ILogger<UpdateFixedAssetIdentifiedCommandValidator> logger)
    {
        RuleFor(command => command.Id).NotEmpty();

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}

