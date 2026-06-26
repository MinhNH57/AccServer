
using Dapper;
using System.Data.Common;

namespace Supply.Infrastructure.Repositories;

public class SUIncrementRepository(SupplyDbContext context)
        : ISUIncrementRepository
{
    private readonly SupplyDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public SUIncrement Add(SUIncrement suIncrement)
    {
        return _context.SUIncrements.Add(suIncrement).Entity;
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

    public async Task<SUIncrement> GetAsync(Guid id)
    {
        var suIncrement = await _context.SUIncrements.FindAsync(id);

        if (suIncrement != null)
        {
            await _context.Entry(suIncrement)
                .Collection(i => i.SUIncrementDetailDepartments)
                .LoadAsync();

            await _context.Entry(suIncrement)
                .Collection(i => i.SUIncrementDetailAllocations)
                .LoadAsync();

            await _context.Entry(suIncrement)
                .Collection(i => i.SUIncrementDetails)
                .LoadAsync();

            await _context.Entry(suIncrement)
                .Collection(i => i.SUIncrementDetailSources)
                .LoadAsync();
        }

        return suIncrement;
    }

    public void Update(SUIncrement suIncrement)
    {
        _context.Entry(suIncrement).State = EntityState.Modified;
    }
}
