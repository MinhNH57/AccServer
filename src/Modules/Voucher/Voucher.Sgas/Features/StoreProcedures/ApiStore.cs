using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Voucher.Sgas.Features.StoreProcedures.GetDataForShiftNotebook;
using Voucher.Sgas.Features.StoreProcedures.GetDataForShiftNotebookByDate;
using Voucher.Sgas.Features.StoreProcedures.GetInventoryForTrading;
using Voucher.Sgas.Model.Store;

namespace Voucher.Sgas.Features.StoreProcedures;
public class ApiStore : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/sgas/").HasApiVersion(1.0).RequireAuthorization();

        api.MapPost("get-data-for-shift-notebook", GetDataForShiftNoteBook)
            .WithSummary("Lấy thông tin dữ liệu mua hàng từ store.")
            .WithDescription("Lấy thông tin hóa dữ liệu mua hàng từ store.")
            .WithTags("SgasVoucher");

        api.MapPost("get-data-for-shift-notebook-by-date", GetDataForShiftNoteBookByDate)
            .WithSummary("Lấy thông tin dữ liệu mua hàng từ store theo ngày.")
            .WithDescription("Lấy thông tin hóa dữ liệu mua hàng từ store theo ngày.")
            .WithTags("SgasVoucher");

        api.MapPost("get-inventory-for-trading", GetInventoryForTrading)
            .WithSummary("Lấy thông tin dữ liệu mua hàng từ store theo ngày.")
            .WithDescription("Lấy thông tin hóa dữ liệu mua hàng từ store theo ngày.")
            .WithTags("SgasVoucher");
    }
    private async Task<IResult> GetDataForShiftNoteBook(
        [AsParameters] VoucherServices services,
        StoreRequestModel request)
    {
        var command = new GetDataForShiftNoteBookCommand(request);
        var result = await services.Mediator.Send(command);

        return Results.Ok(result);
    }
    private async Task<IResult> GetDataForShiftNoteBookByDate(
        [AsParameters] VoucherServices services,
        StoreRequestModel request)
    {
        var command = new GetDataForShiftNoteBookByDateCommand(request);
        var result = await services.Mediator.Send(command);

        return Results.Ok(result);
    }
    private async Task<IResult> GetInventoryForTrading(
        [AsParameters] VoucherServices services,
        GetInventoryForTradingRequest request)
    {
        var command = new GetInventoryForTradingCommand(request);
        var result = await services.Mediator.Send(command);

        return Results.Ok(result);
    }
}
