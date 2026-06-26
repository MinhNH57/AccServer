using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Queries.GetContentPaymentRq;

internal record GetContentPaymentRequiredQuery(Guid Id) : IQuery<Result>;



internal class GetContentPaymentRequiredQueryHandler(VoucherDbContext dbContext, ICurrentUser currentUser) : IQueryHandler<GetContentPaymentRequiredQuery, Result>
{
    public async Task<Result> Handle(GetContentPaymentRequiredQuery query, CancellationToken cancellationToken)
    {
        var data = await dbContext.RequiPaymentData
            .AsNoTracking()
            .AsSplitQuery()
            .Where(c => c.Id == query.Id)
            .Include(x => x.DataContentsList)
            .Include(c => c.HeadInvoiceInputs)
            .Include(z => z.PaymentPlanContents)
            .Include(c => c.RequirePaymentMoneys)
            .Include(c => c.TravelExpensess)
            .FirstOrDefaultAsync( cancellationToken);



        if (data is null) return Result.Failure(new(ErrorCode.BadRequest, ErrorCode.BadRequest));

        if(data.HeadInvoiceInputs is not null)
        {
            data.HeadInvoiceInputs = data.HeadInvoiceInputs.OrderBy(c=>c.InvoiceDate).ToList();
        }
        if(data.TravelExpensess is not null)
        {
            data.TravelExpensess = data.TravelExpensess.OrderBy(c=>c.WorkingDate).ToList();
        }

        //var contents = await dbContext.RequiPaymentDataContents
        //   .AsNoTracking()
        //   .Where(c => c.IdContents == query.Id)
        //   .ToListAsync(cancellationToken: cancellationToken);

        return Result.Success(data);
    }
}

public class GetContentPaymentRequiredEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("vouchers/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapGet("get-payemnt-rq-content/{id}", GetVoucherContent)
            .WithSummary("Lấy voucher bằng Store")
            .WithTags("Vouchers");
    }

    private async Task<IResult> GetVoucherContent([AsParameters] VoucherServices services,
        Guid id,
        CancellationToken token)
    {
        var query = new GetContentPaymentRequiredQuery(id);

        var result = await services.Mediator.Send(query, token);

        return Results.Ok(result);
    }
}