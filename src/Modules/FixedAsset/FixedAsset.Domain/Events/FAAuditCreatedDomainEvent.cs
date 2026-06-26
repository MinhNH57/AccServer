namespace FixedAsset.Domain.Events;

public record class FAAuditCreatedDomainEvent(FAAudit FAAudit) : INotification;
