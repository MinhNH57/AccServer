namespace Supply.Domain.Events;

public record class SUTransferCreatedDomainEvent(SUTransfer SUTransfer) : INotification;
