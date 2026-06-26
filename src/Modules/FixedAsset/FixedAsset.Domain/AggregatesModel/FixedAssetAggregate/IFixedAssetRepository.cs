namespace FixedAsset.Domain.AggregatesModel.FixedAssetAggregate;

public interface IFixedAssetRepository : IRepository<FixedAsset>
{
    FixedAsset Add(FixedAsset fixedAsset);

    void Update(FixedAsset fixedAsset);

    Task<FixedAsset> GetAsync(Guid id);

    Task DeleteAsync(List<Guid> refIds, int refType);

    Task<List<Guid>> CheckExistedConstrain(List<Guid> refIds, int refType);
}

