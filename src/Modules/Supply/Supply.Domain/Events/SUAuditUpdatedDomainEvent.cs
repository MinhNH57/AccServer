namespace Supply.Domain.Events;

public record class SUAuditUpdatedDomainEvent(SUAudit SUAudit) : INotification;
