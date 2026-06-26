namespace FixedAsset.Domain.Events;

public record class FATransferCreatedDomainEvent(FATransfer FATransfer) : INotification;
