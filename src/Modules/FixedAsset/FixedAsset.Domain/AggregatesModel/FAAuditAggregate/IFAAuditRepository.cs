namespace FixedAsset.Domain.AggregatesModel.FAAuditAggregate;

public interface IFAAuditRepository : IRepository<FAAudit>
{
    FAAudit Add(FAAudit fAAudit);

    void Update(FAAudit fAAudit);

    Task<FAAudit> GetAsync(Guid id);

    Task DeleteAsync(List<Guid> refIds, int refType);

    Task<List<Guid>> CheckExistedConstrain(List<Guid> refIds, int refType);
}

