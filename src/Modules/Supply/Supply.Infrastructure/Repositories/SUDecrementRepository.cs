using Dapper;
using System.Data.Common;

namespace Supply.Infrastructure.Repositories;

public class SUDecrementRepository(SupplyDbContext context)
        : ISUDecrementRepository
{
    private readonly SupplyDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public SUDecrement Add(SUDecrement suDecrement)
    {
        return _context.SUDecrements.Add(suDecrement).Entity;
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

    public async Task<SUDecrement> GetAsync(Guid id)
    {
        var suDecrement = await _context.SUDecrements.FindAsync(id);

        if (suDecrement != null)
        {
            await _context.Entry(suDecrement)
                .Collection(i => i.SUDecrementDetails)
                .LoadAsync();
        }

        return suDecrement;
    }

    public void Update(SUDecrement suDecrement)
    {
        _context.Entry(suDecrement).State = EntityState.Modified;
    }
}
