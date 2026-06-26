using Dapper;
using System.Data.Common;

namespace Supply.Infrastructure.Repositories;

public class SUTransferRepository(SupplyDbContext context)
        : ISUTransferRepository
{
    private readonly SupplyDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public SUTransfer Add(SUTransfer suTransfer)
    {
        return _context.SUTransfers.Add(suTransfer).Entity;
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

    public async Task<SUTransfer> GetAsync(Guid refId)
    {
        var suTransfer = await _context.SUTransfers.FindAsync(refId);

        if (suTransfer != null)
        {
            await _context.Entry(suTransfer)
                .Collection(i => i.SUTransferDetails)
                .LoadAsync();
        }

        return suTransfer;
    }

    public void Update(SUTransfer suTransfer)
    {
        _context.Entry(suTransfer).State = EntityState.Modified;
    }
}
