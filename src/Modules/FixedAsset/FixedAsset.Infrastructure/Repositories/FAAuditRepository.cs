using Dapper;
using System.Data.Common;

namespace FixedAsset.Infrastructure.Repositories;

public class FAAuditRepository(FixedAssetDbContext context)
        : IFAAuditRepository
{
    private readonly FixedAssetDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public FAAudit Add(FAAudit fAAudit)
    {
        return _context.FAAudits.Add(fAAudit).Entity;
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

    public async Task<FAAudit> GetAsync(Guid refId)
    {
        var fAAudit = await _context.FAAudits.FindAsync(refId);

        if (fAAudit != null)
        {
            await _context.Entry(fAAudit)
                .Collection(i => i.FAAuditDetails)
                .LoadAsync();
        }

        return fAAudit;
    }

    public void Update(FAAudit fAAudit)
    {
        _context.Entry(fAAudit).State = EntityState.Modified;
    }
}
