using System.Data;
using BuildingBlocks.Dapper;

namespace Voucher.HRM.Infrastructure.StoredProcedures;

public class DeleteVoucherStore : StoredProcedureBase
{
    private const string StoreName = "DeleteData";

    private const string Parameter = "@Parameter";

    private const string TableName = "@TableName";

    private const string KeyData = "@KeyData";

    private const string DataPlus = "@DataPlus";
    private const string MaUser = "@MaUser";

    private const string CodeUnit = "@CodeUnit";


    public DeleteVoucherStore(string parameter, string? tableName, string? keyData, string dataPlus, string maUser,
        int codeUnit
    )
        : base(StoreName)
    {
        Parameters.Add(Parameter, parameter, DbType.String);
        Parameters.Add(TableName, tableName, DbType.String);
        Parameters.Add(KeyData, keyData, DbType.String);
        Parameters.Add(DataPlus, dataPlus, DbType.String);
        Parameters.Add(MaUser, maUser, DbType.String);
        Parameters.Add(CodeUnit, codeUnit, DbType.Int16);
    }
}