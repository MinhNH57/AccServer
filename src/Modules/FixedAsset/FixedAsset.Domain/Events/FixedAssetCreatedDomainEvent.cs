namespace FixedAsset.Domain.Events;

public record class FixedAssetCreatedDomainEvent(
    AggregatesModel.FixedAssetAggregate.FixedAsset FixedAsset) : INotification;
