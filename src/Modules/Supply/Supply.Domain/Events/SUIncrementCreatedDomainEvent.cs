namespace Supply.Domain.Events;

public record class SUIncrementCreatedDomainEvent(SUIncrement SUIncrement) : INotification;
