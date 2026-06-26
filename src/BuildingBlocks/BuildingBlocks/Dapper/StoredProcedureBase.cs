using Dapper;

namespace BuildingBlocks.Dapper;

public abstract class StoredProcedureBase(string storedProcedureName)
{
    public string StoredProcedureName { get; set; } = storedProcedureName;

    public DynamicParameters Parameters { get; } = new();
}
