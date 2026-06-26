namespace FixedAsset.Domain.Events;

public record class FAAdjustmentUpdatedDomainEvent(FAAdjustment FAAdjustment) : INotification;
