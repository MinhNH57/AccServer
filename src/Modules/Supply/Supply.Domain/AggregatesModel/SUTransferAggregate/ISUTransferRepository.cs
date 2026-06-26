namespace Supply.Domain.AggregatesModel.SUTransferAggregate;

public interface ISUTransferRepository : IRepository<SUTransfer>
{
    SUTransfer Add(SUTransfer suTransfer);

    void Update(SUTransfer suTransfer);

    Task<SUTransfer> GetAsync(Guid id);

    Task DeleteAsync(List<Guid> refIds, int refType);

    Task<List<Guid>> CheckExistedConstrain(List<Guid> refIds, int refType);
}

