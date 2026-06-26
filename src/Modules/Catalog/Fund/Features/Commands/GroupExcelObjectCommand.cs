using System.Data;
using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Catalog.Fund.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Fund.Features.Commands;

public class GroupExcelObjectStore : StoredProcedureBase
{
    private const string Parameter = "@Parameter";
    private const string Id = "@Id";
    private const string UserCode = "@UserCode";
    private const string CodeUnit = "@CodeUnit";
    private const string BeginDate = "@BeginDate";
    private const string EndDate = "@EndDate";
    private const string SmartSoftware = "@SmartSoftware";

    public GroupExcelObjectStore(string parameter, string? id, string? userCode, int codeUnit,
        string beginDate, string endDate, string smartSoftware)
        : base("GroupExcelObject")
    {
        Parameters.Add(Parameter, parameter, DbType.String);
        Parameters.Add(Id, id, DbType.String);
        Parameters.Add(UserCode, userCode, DbType.String);
        Parameters.Add(CodeUnit, codeUnit, DbType.Int16);
        Parameters.Add(BeginDate, beginDate, DbType.String);
        Parameters.Add(EndDate, endDate, DbType.String);
        Parameters.Add(SmartSoftware, smartSoftware, DbType.String);
    }
}

public record GroupExcelObjectCommand(string Type, string NumberImports) : ICommand<Result>;

public class GroupExcelObjectCommandHandler(CatalogFundContext dbContext, ICurrentUser currentUser, SmartDataServices smartDataServices) : ICommandHandler<GroupExcelObjectCommand, Result>
{
    public async Task<Result> Handle(GroupExcelObjectCommand command, CancellationToken cancellationToken)
    {
        string numberImport = string.Empty;
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        numberImport = $"CataObj-{timestamp}-{currentUser.CodeUnit}-{currentUser.CodeUser}";
        if (command.Type == "all")
        {
            string firstDayOfCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("MM-dd-yyyy");
            string dateNow = DateTime.Now.ToString("MM-dd-yyyy");
            var store = new GroupExcelObjectStore("GroupExcelObj", command.NumberImports, currentUser.CodeUser, 888, firstDayOfCurrentMonth, dateNow,
                numberImport);

            await smartDataServices.ExcuteNonQueryAsync(store.StoredProcedureName, dbContext.Database.GetConnectionString(),
                store.Parameters);
        }
        else
        {
            var store = new GroupExcelObjectStore("GroupExcelObj", command.NumberImports, currentUser.CodeUser, 888, "", "",
                numberImport);

            await smartDataServices.ExcuteNonQueryAsync(store.StoredProcedureName, dbContext.Database.GetConnectionString(),
                  store.Parameters);
        }

        return Result.Success("ok");
    }
}