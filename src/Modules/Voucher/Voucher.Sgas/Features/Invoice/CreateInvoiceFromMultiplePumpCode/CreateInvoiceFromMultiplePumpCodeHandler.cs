using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.SmartMapper;
using BuildingBlocks.Web;
using System.Data;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Voucher.Sgas.Features.Invoice.CreateInvoiceFromPumpCode;
using Voucher.Sgas.Infrastructure;
using Voucher.Sgas.Model.Invoice;
using Voucher.Sgas.SmartExtension;

namespace Voucher.Sgas.Features.Invoice.CreateInvoiceFromMultiplePumpCode;


public record class CreateInvoiceFromMultiplePumpCodeCommand(CreateInvoiceFromMultiplePumpCodesRequest Request) : IQuery<Result>;



public class CreateInvoiceFromMultiplePumpCodeHandler(VoucherSgasDbContext dbContext, IMappingService mapping, SmartDataServices smartDataServices, ICurrentUser currentUser) : IQueryHandler<CreateInvoiceFromMultiplePumpCodeCommand, Result>
{
    public async Task<Result> Handle(CreateInvoiceFromMultiplePumpCodeCommand request, CancellationToken cancellationToken)
    {

        var pumpCodes = request.Request.Ids.Select(id => new PumpCodeMerge() { CodeUnit = currentUser.CodeUnit, StationId = currentUser.StationId ?? string.Empty, CompanyId = "", PumpId = id.ToString() }).ToList();
         
        var values = new
        {
            Parameter = request.Request.Parameter,
            Id = request.Request.Ids.FirstOrDefault(Guid.NewGuid()),
            Quantity = 5,
            UserCode = currentUser.CodeUser,
            CodeUnit = currentUser.CodeUnit,
            AccountSymbol = "1561",
            BeginDate = ConverterExtension.SmartConvertDatetime(request.Request.BeginDate) + " " + request.Request.BeginDate.TimeOfDay.ToString(),
            EndDate = ConverterExtension.SmartConvertDatetime(request.Request.EndDate) + " " + request.Request.EndDate.TimeOfDay.ToString(),
            Date = ConverterExtension.SmartConvertDatetime(request.Request.EndDate),
            WareHouseCode = currentUser.WarehoseCode,
            ListPumpCodeInv = ConverterExtension.ToDataTable(pumpCodes),
        };

        var response = await smartDataServices.ExecQueryListAsync<object>(request.Request.StoreName,
            dbContext.Database.GetConnectionString()!, values, cancellationToken);
        //var response = await dbContext..QueryAsync("CreateInvoiceFromPumpCode", values);

        return Result.Success(response);
    }
}
