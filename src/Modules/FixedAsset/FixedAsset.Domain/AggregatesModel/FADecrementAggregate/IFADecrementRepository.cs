namespace FixedAsset.Domain.AggregatesModel.FADecrementAggregate;

public interface IFADecrementRepository : IRepository<FADecrement>
{
    FADecrement Add(FADecrement fADecrement);

    void Update(FADecrement fADecrement);

    Task<FADecrement> GetAsync(Guid id);

    Task DeleteAsync(List<Guid> refIds, int refType);

    Task<List<Guid>> CheckExistedConstrain(List<Guid> refIds, int refType);
}

