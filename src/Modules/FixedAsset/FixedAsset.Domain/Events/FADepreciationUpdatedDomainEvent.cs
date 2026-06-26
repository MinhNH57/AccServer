namespace FixedAsset.Domain.Events;

public record class FADepreciationUpdatedDomainEvent(FADepreciation FADepreciation) : INotification;
