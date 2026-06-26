namespace FixedAsset.Domain.Events;

public record class FADepreciationCreatedDomainEvent(FADepreciation FADepreciation) : INotification;
