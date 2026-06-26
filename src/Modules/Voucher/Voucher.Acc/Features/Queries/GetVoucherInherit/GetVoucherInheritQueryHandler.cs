using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Queries.GetVoucherInherit;

internal class GetVoucherInheritQueryHandler(VoucherDbContext dbContext, SmartDataServices smartDataServices, ICurrentUser currentUser) : IQueryHandler<GetVoucherInheritQuery, Result>
{
    public async Task<Result> Handle(GetVoucherInheritQuery query, CancellationToken cancellationToken)
    {
        var param = new
        {
            Parameter = query.Parameter,
            DataType = query.DataType,
            DataTypeInherit = query.DataTypeInherit,
            Id = query.Id,
            BeginDate = query.BeginDate,
            EndDate = query.EndDate,
            UserCode = currentUser.CodeUser,
            CodeUnit =currentUser.CodeUnit,
            ObjectCode = query.ObjectCode,
            IsTaxAccounting = query.IsTaxAccounting,
            IsAccountsPayable = query.IsAccountsPayable
        };
        var data = await smartDataServices.GetListObject<object>("Proc_GetVoucherForInherit", dbContext.Database.GetConnectionString()!, param, cancellationToken);

        return Result.Success(data);
    }
}