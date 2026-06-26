namespace FixedAsset.Domain.Events;

public record class FixedAssetUpdatedDomainEvent(
    AggregatesModel.FixedAssetAggregate.FixedAsset FixedAsset) : INotification;
