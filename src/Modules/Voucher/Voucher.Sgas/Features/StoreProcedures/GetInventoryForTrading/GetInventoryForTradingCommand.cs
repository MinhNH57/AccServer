using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.SmartMapper;
using BuildingBlocks.Web;
using Cassandra.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Voucher.Sgas.Features.Invoice.CreateInvoiceFromMultiplePumpCode;
using Voucher.Sgas.Infrastructure;
using Voucher.Sgas.Model.Invoice;
using Voucher.Sgas.Model.Store;
using Voucher.Sgas.SmartExtension;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Voucher.Sgas.Features.StoreProcedures.GetInventoryForTrading;

//GetInventoryForTradingRequest

public record GetInventoryForTradingCommand(GetInventoryForTradingRequest Request) : IQuery<Result>;

public class GetInventoryForTradingHandler(VoucherSgasDbContext dbContext, IMappingService mapping, SmartDataServices smartDataServices, ICurrentUser currentUser) : IQueryHandler<GetInventoryForTradingCommand, Result>
{
    public async Task<Result> Handle(GetInventoryForTradingCommand request, CancellationToken cancellationToken)
    {
        //ALTER PROCEDURE[dbo].[GetInventoryForTrading] @Parameter nvarchar(50),@Id nvarchar(50),@UserCode nvarchar(20), @CodeUnit int, @WarehoseCode nvarchar(20),@ProductCode nvarchar(20)= '',@CodeSupplier nvarchar(30)= '', @Date nvarchar(10), @ListProduct nvarchar(max),@OnlyExists bit = 0 
        var values = new
        {
            Parameter = request.Request.Parameter,
            Id = request.Request.Id,
            UserCode = currentUser.CodeUser,
            CodeUnit = currentUser.CodeUnit,
            WarehoseCode = currentUser.WarehoseCode,
            ProductCode = request.Request.ProductCode,
            CodeSupplier = request.Request.CodeSupplier,
            Date = ConverterExtension.SmartConvertDatetime(request.Request.Date),
            ListProduct = request.Request.
            OnlyExists = request.Request.OnlyExists,
        };

        var response = await smartDataServices.ExecQueryListAsync<object>("GetInventoryForTrading",
            dbContext.Database.GetConnectionString()!, values, cancellationToken);
        //var response = await dbContext..QueryAsync("CreateInvoiceFromPumpCode", values);

        return Result.Success(response);
    }
}
