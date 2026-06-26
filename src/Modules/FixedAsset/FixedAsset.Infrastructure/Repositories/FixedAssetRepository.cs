using Dapper;
using System.Data.Common;

namespace FixedAsset.Infrastructure.Repositories;

public class FixedAssetRepository(FixedAssetDbContext context)
        : IFixedAssetRepository
{
    private readonly FixedAssetDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public Domain.AggregatesModel.FixedAssetAggregate.FixedAsset Add(Domain.AggregatesModel.FixedAssetAggregate.FixedAsset fixedAsset)
    {
        return _context.FixedAssets.Add(fixedAsset).Entity;
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

    public async Task<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset> GetAsync(Guid id)
    {
        var fixedAsset = await _context.FixedAssets.FindAsync(id);

        if (fixedAsset != null)
        {
            await _context.Entry(fixedAsset)
                .Collection(i => i.FixedAssetDetailAllocations)
                .LoadAsync();

            await _context.Entry(fixedAsset)
                .Collection(i => i.FixedAssetDetailSources)
                .LoadAsync();

            await _context.Entry(fixedAsset)
                .Collection(i => i.FixedAssetDetails)
                .LoadAsync();

            await _context.Entry(fixedAsset)
                .Collection(i => i.FixedAssetDetailAccessories)
                .LoadAsync();
        }

        return fixedAsset;
    }

    public void Update(Domain.AggregatesModel.FixedAssetAggregate.FixedAsset fixedAsset)
    {
        _context.Entry(fixedAsset).State = EntityState.Modified;
    }
}
