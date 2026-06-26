using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.ToKhai01;

public class ToKhai01Endpont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapGet("/tokhai01", ToKhai01)
            .WithTags("Vouchers");
        api.MapPost("/pdf", ExportPdf).WithTags("Vouchers");
    }

    private async Task<IResult> ToKhai01([AsParameters] VoucherServices services, [AsParameters] ToKhai01Request request, CancellationToken token)
    {
        var command = new ToKhai01Command(request);
        var result = await services.Mediator.Send(command, token);
        return Results.Ok(result);
    }
    private IResult ExportPdf(VatPrintDto dto)
    {
        if (dto == null) dto = new VatPrintDto();
        QuestPDF.Settings.License = LicenseType.Community;
        var doc = new VatFormDocument(dto);
        var bytes = doc.GeneratePdf();
        var fileName = $"ToKhai_GTGT_01_{DateTime.Now:yyyyMMdd_HHmm}.pdf";
        return Results.File(bytes, "application/pdf", fileName);
    }
}