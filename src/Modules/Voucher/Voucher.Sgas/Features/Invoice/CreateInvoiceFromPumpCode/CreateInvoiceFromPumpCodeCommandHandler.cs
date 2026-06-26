using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.SmartMapper;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Voucher.Sgas.Infrastructure;
using Voucher.Sgas.Model.Invoice;
using Voucher.Sgas.SmartExtension;

namespace Voucher.Sgas.Features.Invoice.CreateInvoiceFromPumpCode;


public record class CreateInvoiceFromPumpCodeCommand(CreateInvoiceRequest Request) : IQuery<Result>;


public class CreateInvoiceFromPumpCodeCommandHandler(VoucherSgasDbContext dbContext, IMappingService mapping, SmartDataServices smartDataServices, ICurrentUser currentUser) : IQueryHandler<CreateInvoiceFromPumpCodeCommand, Result>
{
    public async Task<Result> Handle(CreateInvoiceFromPumpCodeCommand request, CancellationToken cancellationToken)
    {
        var pumpCode = await dbContext.SmartDataPumpCode
            .AsNoTracking().AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.Request.Id, cancellationToken: cancellationToken);

        if (pumpCode is null)
        {
            return Result.Failure(new Error(ErrorCode.NotFound, "Không tìm thấy mã bơm: " + string.Join(",", request.Request.Id)));
        }
        var values = new
        {
            Parameter = request.Request.Parameter,
            Id = request.Request.Id,
            Quantity = 5,
            UserCode = currentUser.NameUser,
            CodeUnit = currentUser.CodeUnit,
            AccountSymbol = "1561",
            BeginDate = ConverterExtension.SmartConvertDatetime((DateTime)pumpCode.RecordDate),
            EndDate = ConverterExtension.SmartConvertDatetime((DateTime)pumpCode.RecordDate),
            Date = ConverterExtension.SmartConvertDatetime((DateTime)pumpCode.RecordDate),
            WareHouseCode = currentUser.WarehoseCode,
            ProductCode = pumpCode.ProductCode,
        };
        var response = await smartDataServices.ExecQueryListAsync<CreateInvoiceFromPumpCodeResponse>("CreateInvoiceFromPumpCode",
            dbContext.Database.GetConnectionString()!, values, cancellationToken);
        //var response = await dbContext..QueryAsync("CreateInvoiceFromPumpCode", values);

        return Result.Success(response);
    }
}
