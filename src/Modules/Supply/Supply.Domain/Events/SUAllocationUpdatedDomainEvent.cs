namespace Supply.Domain.Events;

public record class SUAllocationUpdatedDomainEvent(SUAllocation SUAllocation) : INotification;
