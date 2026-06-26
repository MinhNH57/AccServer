namespace FixedAsset.Domain.AggregatesModel.FAChangeFinancialLeasingToOwnerAggregate;

public interface IFAChangeFinancialLeasingToOwnerRepository : IRepository<FAChangeFinancialLeasingToOwner>
{
    FAChangeFinancialLeasingToOwner Add(FAChangeFinancialLeasingToOwner fAChangeFinancialLeasingToOwner);

    void Update(FAChangeFinancialLeasingToOwner fAChangeFinancialLeasingToOwner);

    Task<FAChangeFinancialLeasingToOwner> GetAsync(Guid id);

    Task DeleteAsync(List<Guid> refIds, int refType);

    Task<List<Guid>> CheckExistedConstrain(List<Guid> refIds, int refType);
}

