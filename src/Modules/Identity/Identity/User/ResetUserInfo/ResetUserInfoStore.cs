using System.Data;
using BuildingBlocks.Dapper;

namespace Identity.User.ResetUserInfo;

public class ResetUserInfoStore : StoredProcedureBase
{
    public const string Password = "@Password";
    public const string IdDevice = "@IdDevice";
    public const string Rollcal = "@Rollcal";
    public const string DeleteRollcal = "@DeleteRollcal";
    public const string CodeUser = "@CodeUser";
    public ResetUserInfoStore(bool password, bool idDevice, bool rollcal, bool deleteRollcal, string codeUser) : base("ResetUserInfo")
    {
        Parameters.Add(Password, password, DbType.Boolean);
        Parameters.Add(IdDevice, idDevice, DbType.Boolean);
        Parameters.Add(Rollcal, rollcal, DbType.Boolean);
        Parameters.Add(DeleteRollcal, deleteRollcal, DbType.Boolean);
        Parameters.Add(CodeUser, codeUser, DbType.String);
    }
}
