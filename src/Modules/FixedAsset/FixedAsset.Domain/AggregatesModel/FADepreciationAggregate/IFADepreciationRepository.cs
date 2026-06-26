namespace FixedAsset.Domain.AggregatesModel.FADepreciationAggregate;

public interface IFADepreciationRepository : IRepository<FADepreciation>
{
    FADepreciation Add(FADepreciation fADepreciation);

    void Update(FADepreciation fADepreciation);

    Task<FADepreciation> GetAsync(Guid id);

    Task DeleteAsync(List<Guid> refIds, int refType);

    Task<List<Guid>> CheckExistedConstrain(List<Guid> refIds, int refType);
}

