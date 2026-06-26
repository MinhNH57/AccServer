namespace Supply.Domain.Events;

public record class SUAuditCreatedDomainEvent(SUAudit SuAudit) : INotification;
