using Dapper;
using System.Data.Common;

namespace Supply.Infrastructure.Repositories;

public class SUAllocationRepository(SupplyDbContext context)
        : ISUAllocationRepository
{
    private readonly SupplyDbContext _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public SUAllocation Add(SUAllocation suAllocation)
    {
        return _context.SUAllocations.Add(suAllocation).Entity;
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

    public async Task<SUAllocation> GetAsync(Guid id)
    {
        var suAllocation = await _context.SUAllocations.FindAsync(id);

        if (suAllocation != null)
        {
            await _context.Entry(suAllocation)
                .Collection(i => i.SUAllocationDetailExpenses)
                .LoadAsync();

            await _context.Entry(suAllocation)
                .Collection(i => i.SUAllocationDetailTables)
                .LoadAsync();

            await _context.Entry(suAllocation)
                .Collection(i => i.SUAllocationDetailPosts)
                .LoadAsync();
        }

        return suAllocation;
    }

    public void Update(SUAllocation suAllocation)
    {
        _context.Entry(suAllocation).State = EntityState.Modified;
    }
}
