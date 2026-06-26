namespace Supply.Domain.AggregatesModel.SUAdjustmentAggregate;

public interface ISUAdjustmentRepository : IRepository<SUAdjustment>
{
    SUAdjustment Add(SUAdjustment suAdjustment);

    void Update(SUAdjustment suAdjustment);

    Task<SUAdjustment> GetAsync(Guid id);

    Task DeleteAsync(List<Guid> refIds, int refType);

    Task<List<Guid>> CheckExistedConstrain(List<Guid> refIds, int refType);
}

