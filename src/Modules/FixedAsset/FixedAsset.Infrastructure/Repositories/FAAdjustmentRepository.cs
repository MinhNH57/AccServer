using Dapper;
using System.Data.Common;

namespace FixedAsset.Infrastructure.Repositories;

public class FAAdjustmentRepository(FixedAssetDbContext context)
        : IFAAdjustmentRepository
{
    private readonly FixedAssetDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public FAAdjustment Add(FAAdjustment fAAdjustment)
    {
        return _context.FAAdjustments.Add(fAAdjustment).Entity;
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

    public async Task<FAAdjustment> GetAsync(Guid refId)
    {
        var fAAdjustment = await _context.FAAdjustments.FindAsync(refId);

        if (fAAdjustment != null)
        {
            await _context.Entry(fAAdjustment)
                .Collection(i => i.FAAdjustmentDetails)
                .LoadAsync();

            await _context.Entry(fAAdjustment)
                .Collection(i => i.FAAdjustmentDetailPosts)
                .LoadAsync();
        }

        return fAAdjustment;
    }

    public void Update(FAAdjustment fAAdjustment)
    {
        _context.Entry(fAAdjustment).State = EntityState.Modified;
    }
}
