namespace FixedAsset.Domain.Events;

public record class FAAdjustmentCreatedDomainEvent(FAAdjustment FAAdjustment) : INotification;
