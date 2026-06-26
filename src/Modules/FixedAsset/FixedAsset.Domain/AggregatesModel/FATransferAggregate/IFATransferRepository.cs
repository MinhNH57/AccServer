namespace FixedAsset.Domain.AggregatesModel.FATransferAggregate;

public interface IFATransferRepository : IRepository<FATransfer>
{
    FATransfer Add(FATransfer fATransfer);

    void Update(FATransfer fATransfer);

    Task<FATransfer> GetAsync(Guid id);

    Task DeleteAsync(List<Guid> refIds, int refType);

    Task<List<Guid>> CheckExistedConstrain(List<Guid> refIds, int refType);
}

