using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Voucher.Sgas.Features.SalesData.CreateSalesSmartData;
using Voucher.Sgas.Features.SalesData.GetSalesSmartData;
using Voucher.Sgas.Features.SalesData.UpdateSalesSmartData;
using Voucher.Sgas.Model.Sales;

namespace Voucher.Sgas.Features.SalesData;
public class ApiSales : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/sgas/").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("get-sales-data-by-id/{id:guid}", GetSaleDataById)
            .WithSummary("Lấy thông tin hóa đơn bán hàng.")
            .WithDescription("Lấy thông tin hóa đơn bán hàng theo số chứng Id.")
            .WithTags("SgasVoucher");

        api.MapPost("create-sales-smart-data", CreateSalesSmartData)
            .WithSummary("Tạo hóa đơn bán hàng.")
            .WithDescription("Tạo hóa đơn bán hàng")
            .WithTags("SgasVoucher");

        api.MapPut("update-sales-smart-data", UpdateSalesSmartData)
            .WithSummary("Cập nhật hóa đơn bán hàng.")
            .WithDescription("Cập nhật hóa đơn bán hàng")
            .WithTags("SgasVoucher");
    }
    private async Task<IResult> GetSaleDataById([AsParameters] VoucherServices services, Guid id, CancellationToken token)
    {
        var query = new GetGetSaleSmartDataQuery(id);
        var result = await services.Mediator.Send(query, token);
        return Results.Ok(result);
    }
    private async Task<IResult> CreateSalesSmartData([AsParameters] VoucherServices services, SalesModelAction modelAction, CancellationToken token)
    {
        var query = new CreateSalesModelAction(modelAction);
        var result = await services.Mediator.Send(query, token);
        return Results.Ok(result);
    }
    private async Task<IResult> UpdateSalesSmartData([AsParameters] VoucherServices services, SalesModelAction modelAction, CancellationToken token)
    {
        var query = new UpdateSalesModelAction(modelAction);
        var result = await services.Mediator.Send(query, token);
        return Results.Ok(result);
    }
}