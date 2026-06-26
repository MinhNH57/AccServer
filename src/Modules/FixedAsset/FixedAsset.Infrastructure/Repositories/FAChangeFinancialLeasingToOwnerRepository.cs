using Dapper;
using System.Data.Common;

namespace FixedAsset.Infrastructure.Repositories;

public class FAChangeFinancialLeasingToOwnerRepository(FixedAssetDbContext context)
        : IFAChangeFinancialLeasingToOwnerRepository
{
    private readonly FixedAssetDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public FAChangeFinancialLeasingToOwner Add(FAChangeFinancialLeasingToOwner fAChangeFinancialLeasingToOwner)
    {
        return _context.FAChangeFinancialLeasingToOwners.Add(fAChangeFinancialLeasingToOwner).Entity;
    }

    public async Task<List<Guid>> CheckExistedConstrain(List<Guid> refIds, int refType)
    {
        var connection = _context.Database.GetDbConnection();
        await EnsureConnectionOpenAsync(connection);

        var existedConstrainIds = await connection.QueryAsync<Guid>(
                "CheckExistedConstrain",
                new
                {
                    RefIds = string.Join(",", refIds),
                    RefType = refType
                },
                transaction: _context.Database.CurrentTransaction?.GetDbTransaction(),
                commandType: CommandType.StoredProcedure
            );

        return [.. existedConstrainIds];
    }

    private static async Task EnsureConnectionOpenAsync(DbConnection connection)
    {
        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();
    }

    public async Task DeleteAsync(List<Guid> refIds, int refType)
    {
        try
        {
            var connection = _context.Database.GetDbConnection();
            await EnsureConnectionOpenAsync(connection);

            await connection.ExecuteAsync(
                    "DeleteVoucher",
                    new
                    {
                        RefIds = string.Join(",", refIds),
                        RefType = refType
                    },
                    transaction: _context.Database.CurrentTransaction?.GetDbTransaction(),
                    commandType: CommandType.StoredProcedure
                );

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<FAChangeFinancialLeasingToOwner> GetAsync(Guid refId)
    {
        var fAChangeFinancialLeasingToOwner = await _context.FAChangeFinancialLeasingToOwners.FindAsync(refId);

        if (fAChangeFinancialLeasingToOwner != null)
        {
            await _context.Entry(fAChangeFinancialLeasingToOwner)
                .Collection(i => i.FAChangeFinancialLeasingToOwnerDetails)
                .LoadAsync();
        }

        return fAChangeFinancialLeasingToOwner;
    }

    public void Update(FAChangeFinancialLeasingToOwner fAChangeFinancialLeasingToOwner)
    {
        _context.Entry(fAChangeFinancialLeasingToOwner).State = EntityState.Modified;
    }
}
