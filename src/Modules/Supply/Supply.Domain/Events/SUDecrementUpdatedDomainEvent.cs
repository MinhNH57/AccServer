namespace Supply.Domain.Events;

public record class SUDecrementUpdatedDomainEvent(SUDecrement SUDecrement) : INotification;
