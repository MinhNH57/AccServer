namespace FixedAsset.Domain.Events;

public record class FATransferUpdatedDomainEvent(FATransfer FATransfer) : INotification;
