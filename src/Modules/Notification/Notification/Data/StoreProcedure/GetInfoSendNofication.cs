using System.Data;
using BuildingBlocks.Dapper;

namespace Notification.Data.StoreProcedure;

public class GetInfoSendNofication : StoredProcedureBase
{
    private const string Parameter = "@Parameter";
    private const string UserNames = "@UserNames";
    private const string CodeUnit = "@CodeUnit";
    private const string DataType = "@DataType";
    private const string Id = "@Id";
    private const string MoreInfo = "@MoreInfo";

    public GetInfoSendNofication(string parameter, string id, string userNames, int codeUnit = 100, string dataType = "", string? moreInfo = null) : base("GetInfoSendNofication")
    {
        Parameters.Add(Parameter, parameter, DbType.String);
        Parameters.Add(UserNames, userNames, DbType.String);
        Parameters.Add(CodeUnit, codeUnit, DbType.Int16);
        Parameters.Add(DataType, dataType, DbType.String);
        Parameters.Add(Id, id, DbType.String);
        Parameters.Add(MoreInfo, moreInfo, DbType.String);
    }
}

public class RecipientOfMessage
{
    public string? UserCode { get; set; }
    public string? UserName { get; set; }
    public string TokenMessage { get; set; } = string.Empty;
    public int Ordinal { get; set; }
    public string? Position { get; set; }
    public string TitleNoti { get; set; } = "SMART SOFTWARE";
    public string BodyNoti { get; set; } = "THÔNG BÁO TỪ PHÀN MỀM";
    public bool IsData { get; set; } = false;
}