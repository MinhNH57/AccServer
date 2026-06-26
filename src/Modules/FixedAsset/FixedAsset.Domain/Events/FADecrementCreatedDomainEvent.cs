namespace FixedAsset.Domain.Events;

public record class FADecrementCreatedDomainEvent(FADecrement FADecrement) : INotification;
