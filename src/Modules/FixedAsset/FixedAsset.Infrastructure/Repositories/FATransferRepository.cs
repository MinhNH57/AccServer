using Dapper;
using System.Data.Common;

namespace FixedAsset.Infrastructure.Repositories;

public class FATransferRepository(FixedAssetDbContext context)
        : IFATransferRepository
{
    private readonly FixedAssetDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public FATransfer Add(FATransfer fADecrement)
    {
        return _context.FATransfers.Add(fADecrement).Entity;
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

    public async Task<FATransfer> GetAsync(Guid refId)
    {
        var fATransfer = await _context.FATransfers.FindAsync(refId);

        if (fATransfer != null)
        {
            await _context.Entry(fATransfer)
                .Collection(i => i.FATransferDetails)
                .LoadAsync();
        }

        return fATransfer;
    }

    public void Update(FATransfer fATransfer)
    {
        _context.Entry(fATransfer).State = EntityState.Modified;
    }
}
