namespace Supply.Domain.AggregatesModel.SUIncrementAggregate;

public interface ISUIncrementRepository : IRepository<SUIncrement>
{
    SUIncrement Add(SUIncrement suIncrement);

    void Update(SUIncrement suIncrement);

    Task<SUIncrement> GetAsync(Guid id);

    Task DeleteAsync(List<Guid> refIds, int refType);

    Task<List<Guid>> CheckExistedConstrain(List<Guid> refIds, int refType);
}

