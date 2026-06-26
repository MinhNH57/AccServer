namespace Supply.Domain.Events;

public record class SUAdjustmentUpdatedDomainEvent(SUAdjustment SUAdjustment) : INotification;
