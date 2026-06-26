namespace Supply.Domain.AggregatesModel.SUAllocationAggregate;

public interface ISUAllocationRepository : IRepository<SUAllocation>
{
    SUAllocation Add(SUAllocation suAllocation);

    void Update(SUAllocation suAllocation);

    Task<SUAllocation> GetAsync(Guid id);

    Task DeleteAsync(List<Guid> refIds, int refType);

    Task<List<Guid>> CheckExistedConstrain(List<Guid> refIds, int refType);
}

