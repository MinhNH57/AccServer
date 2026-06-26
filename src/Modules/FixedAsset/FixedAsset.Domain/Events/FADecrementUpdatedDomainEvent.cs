namespace FixedAsset.Domain.Events;

public record class FADecrementUpdatedDomainEvent(FADecrement FADecrement) : INotification;
