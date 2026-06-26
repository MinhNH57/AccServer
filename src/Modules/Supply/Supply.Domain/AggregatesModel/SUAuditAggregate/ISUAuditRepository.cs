namespace Supply.Domain.AggregatesModel.SUAuditAggregate;

public interface ISUAuditRepository : IRepository<SUAudit>
{
    SUAudit Add(SUAudit suAudit);

    void Update(SUAudit suAudit);

    Task<SUAudit> GetAsync(Guid id);

    Task DeleteAsync(List<Guid> refIds, int refType);

    Task<List<Guid>> CheckExistedConstrain(List<Guid> refIds, int refType);
}

