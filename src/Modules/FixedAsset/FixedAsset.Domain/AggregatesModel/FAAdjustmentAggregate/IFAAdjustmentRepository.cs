namespace FixedAsset.Domain.AggregatesModel.FAAdjustmentAggregate;

public interface IFAAdjustmentRepository : IRepository<FAAdjustment>
{
    FAAdjustment Add(FAAdjustment fAAdjustment);

    void Update(FAAdjustment fAAdjustment);

    Task<FAAdjustment> GetAsync(Guid id);

    Task DeleteAsync(List<Guid> refIds, int refType);

    Task<List<Guid>> CheckExistedConstrain(List<Guid> refIds, int refType);
}

