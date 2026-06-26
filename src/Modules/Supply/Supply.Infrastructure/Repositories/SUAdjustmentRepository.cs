using Dapper;
using System.Data.Common;

namespace Supply.Infrastructure.Repositories;

public class SUAdjustmentRepository(SupplyDbContext context)
        : ISUAdjustmentRepository
{
    private readonly SupplyDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public SUAdjustment Add(SUAdjustment suAdjustment)
    {
        return _context.SUAdjustments.Add(suAdjustment).Entity;
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

    public async Task<SUAdjustment> GetAsync(Guid id)
    {
        var suAdjustment = await _context.SUAdjustments.FindAsync(id);

        if (suAdjustment != null)
        {
            await _context.Entry(suAdjustment)
                .Collection(i => i.SUAdjustmentDetails)
                .LoadAsync();

            await _context.Entry(suAdjustment)
                .Collection(i => i.SUAdjustmentDetailVouchers)
                .LoadAsync();
        }

        return suAdjustment;
    }

    public void Update(SUAdjustment suAdjustment)
    {
        _context.Entry(suAdjustment).State = EntityState.Modified;
    }
}
