namespace Supply.Domain.Events;

public record class SUTransferUpdatedDomainEvent(SUTransfer SUTransfer) : INotification;
