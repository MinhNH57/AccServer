using Dapper;
using System.Data.Common;

namespace Supply.Infrastructure.Repositories;

public class SUAuditRepository(SupplyDbContext context)
        : ISUAuditRepository
{
    private readonly SupplyDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public SUAudit Add(SUAudit suAudit)
    {
        return _context.SUAudits.Add(suAudit).Entity;
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

    public async Task<SUAudit> GetAsync(Guid id)
    {
        var suAudit = await _context.SUAudits.FindAsync(id);

        if (suAudit != null)
        {
            await _context.Entry(suAudit)
                .Collection(i => i.SUAuditDetails)
                .LoadAsync();
        }

        return suAudit;
    }

    public void Update(SUAudit suAudit)
    {
        _context.Entry(suAudit).State = EntityState.Modified;
    }
}
