namespace Supply.Domain.Events;

public record class SUIncrementUpdatedDomainEvent(SUIncrement SUIncrement) : INotification;
