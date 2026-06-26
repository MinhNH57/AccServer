namespace FixedAsset.Domain.Events;

public record class FAAuditUpdatedDomainEvent(FAAudit FAAudit) : INotification;
