using System.Data;
using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Queries.GetVoucherStore;

public class GetVoucherStoreProduce : StoredProcedureBase
{
    public const string Parameter = "@Parameter";
    public const string DataType = "@DataType";
    public const string UserCode = "@UserCode";
    public const string CodeUnit = "@CodeUnit";
    public const string TableName = "@TableName";
    public const string Condition = "@Condition";

    public GetVoucherStoreProduce(string? parameter, string? dataType = "", string? userCode = "", int? codeUnit = 0, string? tableName = "", string condition = "") : base("GetInforVouchers")
    {
        Parameters.Add(Parameter, parameter, DbType.String);
        Parameters.Add(DataType, dataType, DbType.String);
        Parameters.Add(UserCode, userCode, DbType.String);
        Parameters.Add(CodeUnit, codeUnit, DbType.Int16);
        Parameters.Add(TableName, tableName, DbType.String);
        Parameters.Add(Condition, condition, DbType.String);
    }
}

public class GetVoucherQuery
{
    public string Parameter { get; set; } = string.Empty;
    public string DataType { get; set; } = string.Empty;
    public string UserCode { get; set; } = string.Empty;
    public int CodeUnit { get; set; }
    public string TableName { get; set; } = string.Empty;
    public string? Condition { get; set; }

}

public record GetVoucherFromStoreQuery(GetVoucherQuery Query) : ICommand<Result>;

public class GetVoucherFromStoreQueryHandler(SmartDataServices smartData, VoucherDbContext dbContext) : ICommandHandler<GetVoucherFromStoreQuery, Result>
{
    public async Task<Result> Handle(GetVoucherFromStoreQuery command, CancellationToken cancellationToken)
    {
        var voucherStore = new GetVoucherStoreProduce(command.Query.Parameter, command.Query.DataType,
            command.Query.UserCode, command.Query.CodeUnit, command.Query.TableName, command.Query.Condition ?? "");

        var data = await smartData
            .GetListObject<object>(voucherStore.StoredProcedureName, dbContext.Database.GetConnectionString()!, voucherStore.Parameters, cancellationToken);

        return Result.Success(data);
    }
}