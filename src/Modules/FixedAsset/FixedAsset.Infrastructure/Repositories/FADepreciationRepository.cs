using Dapper;
using System.Data.Common;

namespace FixedAsset.Infrastructure.Repositories;

public class FADepreciationRepository(FixedAssetDbContext context)
        : IFADepreciationRepository
{
    private readonly FixedAssetDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public FADepreciation Add(FADepreciation fADepreciation)
    {
        return _context.FADepreciations.Add(fADepreciation).Entity;
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

    public async Task<FADepreciation> GetAsync(Guid id)
    {
        var fADepreciation = await _context.FADepreciations.FindAsync(id);

        if (fADepreciation != null)
        {
            await _context.Entry(fADepreciation)
                .Collection(i => i.FADepreciationDetailAllocations)
                .LoadAsync();

            await _context.Entry(fADepreciation)
                .Collection(i => i.FADepreciationDetails)
                .LoadAsync();

            await _context.Entry(fADepreciation)
                .Collection(i => i.FADepreciationDetailPosts)
                .LoadAsync();
        }

        return fADepreciation;
    }

    public void Update(FADepreciation fADepreciation)
    {
        _context.Entry(fADepreciation).State = EntityState.Modified;
    }
}
