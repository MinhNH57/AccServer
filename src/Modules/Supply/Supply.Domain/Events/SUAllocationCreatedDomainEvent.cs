namespace Supply.Domain.Events;

public record class SUAllocationCreatedDomainEvent(SUAllocation SUAllocation) : INotification;
