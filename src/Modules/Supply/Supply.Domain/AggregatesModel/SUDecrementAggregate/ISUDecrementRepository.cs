namespace Supply.Domain.AggregatesModel.SUDecrementAggregate;

public interface ISUDecrementRepository : IRepository<SUDecrement>
{
    SUDecrement Add(SUDecrement suDecrement);

    void Update(SUDecrement suDecrement);

    Task<SUDecrement> GetAsync(Guid id);

    Task DeleteAsync(List<Guid> refIds, int refType);

    Task<List<Guid>> CheckExistedConstrain(List<Guid> refIds, int refType);
}

