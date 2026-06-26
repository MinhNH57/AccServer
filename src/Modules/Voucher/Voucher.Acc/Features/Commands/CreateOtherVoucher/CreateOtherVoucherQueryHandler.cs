using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Queries.CreateOtherVoucher;

internal class CreateOtherVoucherQueryHandler(VoucherDbContext dbContext, SmartDataServices smartDataServices, ICurrentUser currentUser) : IQueryHandler<CreateOtherVoucherQuery, Result>
{
    public async Task<Result> Handle(CreateOtherVoucherQuery query, CancellationToken cancellationToken)
    {
        //@DataType nvarchar(50),@Id nvarchar(50),@UserCode nvarchar(20),@CodeUnit int,@EndDate nvarchar(10), @NumberOfVouchers nvarchar(20)
        var param = new
        {
            DataType = query.DataType,
            Id = query.Id,
            UserCode = currentUser.CodeUser,
            CodeUnit =currentUser.CodeUnit,
            EndDate = DateTime.Now,
            NumberOfVouchers = query.NumberOfVouchers
        };
        var data = await smartDataServices.ExcuteNonQueryAsync("CreateOtherVoucher", dbContext.Database.GetConnectionString()!, param, cancellationToken);

        return Result.Success(data);
    }
}