using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Queries.GetDataStatistical;


internal record GetDataStatisticalQuery(string Parameter, string Id, string ProductCode, string CodeColor, string CodeSize, string CodeStage) : IQuery<Result>;

internal class GetDataStatisticalQueryHandler(VoucherDbContext dbContext, SmartDataServices smartDataServices, ICurrentUser currentUser) : IQueryHandler<GetDataStatisticalQuery, Result>
{
    private const string StoreName = "GetDataStatistical";
    public async Task<Result> Handle(GetDataStatisticalQuery request, CancellationToken cancellationToken)
    {
        var paramStore = new
        {
            Parameter = request.Parameter,
            TableName = string.Empty,
            Id = request.Id,
            ProductCode = request.ProductCode,
            CodeColor = request.CodeColor,
            CodeSize = request.CodeSize,
            CodeStage = request.CodeStage,
            UserCode = currentUser.CodeUser,
            CodeUnit = 888
        };
        var connStr = dbContext.Database.GetConnectionString();
        var result = await smartDataServices.GetListObject<object>(StoreName, connStr!, paramStore, cancellationToken);

        return Result.Success(result);
    }
}