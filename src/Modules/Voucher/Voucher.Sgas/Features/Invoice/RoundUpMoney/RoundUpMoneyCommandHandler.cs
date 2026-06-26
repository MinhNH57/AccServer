using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.SmartMapper;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Voucher.Sgas.Infrastructure;
using Voucher.Sgas.Model.Invoice;

namespace Voucher.Sgas.Features.Invoice.RoundUpMoney;

public record class RoundUpMoneyCommand(RoundUpMoneyRequest Request) : IQuery<Result>;

public class RoundUpMoneyCommandHandler(VoucherSgasDbContext dbContext, IMappingService mapping, SmartDataServices smartDataServices, ICurrentUser currentUser) : IQueryHandler<RoundUpMoneyCommand, Result>
{ 
    public async Task<Result> Handle(RoundUpMoneyCommand request, CancellationToken cancellationToken)
    {
        await dbContext.Database.ExecuteSqlRawAsync(
            "UPDATE SalesSmartContentsData SET Quantity = {0}, AmountOfMoney = {1} WHERE IdContents = {2}",
            request.Request.Quantity,
            request.Request.AmountOfMoney,
            request.Request.Id
        );
        return Result.Success(true);
    }
}


