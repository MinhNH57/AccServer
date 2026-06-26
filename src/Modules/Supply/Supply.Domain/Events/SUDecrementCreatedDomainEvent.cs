namespace Supply.Domain.Events;

public record class SUDecrementCreatedDomainEvent(SUDecrement SUDecrement) : INotification;
