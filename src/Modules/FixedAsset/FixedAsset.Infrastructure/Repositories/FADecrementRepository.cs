using Dapper;
using System.Data.Common;

namespace FixedAsset.Infrastructure.Repositories;

public class FADecrementRepository(FixedAssetDbContext context)
        : IFADecrementRepository
{
    private readonly FixedAssetDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public FADecrement Add(FADecrement fADecrement)
    {
        return _context.FADecrements.Add(fADecrement).Entity;
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

    public async Task<FADecrement> GetAsync(Guid refId)
    {
        var fADecrement = await _context.FADecrements.FindAsync(refId);

        if (fADecrement != null)
        {
            await _context.Entry(fADecrement)
                .Collection(i => i.FADecrementDetails)
                .LoadAsync();

            await _context.Entry(fADecrement)
                .Collection(i => i.FADecrementDetailPosts)
                .LoadAsync();
        }

        return fADecrement;
    }

    public void Update(FADecrement fADecrement)
    {
        _context.Entry(fADecrement).State = EntityState.Modified;
    }
}
