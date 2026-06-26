using BuildingBlocks.Pagination.Version1;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Voucher.Acc.Features.Commands.SmartData.UpdateVoucher;
using Voucher.Acc.Features.Queries.GetVoucherContent;
using Voucher.Acc.Model;
using Voucher.Acc.Model.DebtOffSet;

namespace Voucher.Acc.Features.Queries.GetDebtOffSet;

public class GetDebtOffSetEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("debtOffSetContents/{idVoucher}", GetVoucherContent)
            .WithName("DebtOffSetContents")
            .WithSummary("Danh sách nội dung phiếu bù trừ.")
            .WithDescription("Nhận danh sách nội dung phiếu bù trừ.")
            .WithTags("Vouchers");
        api.MapPut("update-debtOffSet/{id}", UpdateVoucher)
          .WithTags("Vouchers");
    }

    private async Task<IResult> GetVoucherContent([AsParameters] VoucherServices services,
          Guid idVoucher,
          CancellationToken token)
    {
        var query = new GetDebtOffSetQuery(idVoucher);

        var result = await services.Mediator.Send(query, token);

        return Results.Ok(result);
    }
    private async Task<IResult> UpdateVoucher(
      [AsParameters] VoucherServices services,
      Guid id,
      UpdateDebtOffSetRequest request,
      CancellationToken token)
    {
        request.Id = id;
        var command = new UpdateDebtOffSetCommand(request);
        var result = await services.Mediator.Send(command, token);
        return Results.Ok(result);
    }
}