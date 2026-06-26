namespace Supply.Domain.Events;

public record class SUAdjustmentCreatedDomainEvent(SUAdjustment SuAdjustment) : INotification;
