using System.Data;
using BuildingBlocks.Dapper;

namespace Voucher.Acc.Features.Commands.ConfirmVoucher;

public class ConfirmVoucherStore : StoredProcedureBase
{
    private const string Parameter = "@Parameter";
    private const string TableName = "@TableName";
    private const string Id = "@Id";
    private const string UserCode = "@UserCode";
    private const string CodeUnit = "@CodeUnit";
    private const string Status = "@Status";
    private const string Reason = "@Reason";
    private const string ConfirmVoucher = "@ConfirmVoucher";

    //Status  1: Đã duyệt , 2: Từ chối
    public ConfirmVoucherStore(string parameter, string tableName, string id, string userCode, int codeUnit,string status, string reason ,int? confirmVoucher) : base("ConfirmVoucher")
    {
        Parameters.Add(Parameter, parameter, DbType.String);
        Parameters.Add(TableName, tableName, DbType.String);
        Parameters.Add(Id, id, DbType.String);
        Parameters.Add(UserCode, userCode, DbType.String);
        Parameters.Add(CodeUnit, codeUnit, DbType.Int16);
        Parameters.Add(Status, status, DbType.String);
        Parameters.Add(Reason, reason, DbType.String);
        Parameters.Add(ConfirmVoucher, confirmVoucher, DbType.Int16);
    }
}