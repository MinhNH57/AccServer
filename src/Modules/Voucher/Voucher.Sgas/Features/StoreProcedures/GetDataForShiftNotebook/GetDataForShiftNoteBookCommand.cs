using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response; 
using Microsoft.EntityFrameworkCore;
using Voucher.Sgas.Infrastructure;
using Voucher.Sgas.Model.Store;

namespace Voucher.Sgas.Features.StoreProcedures.GetDataForShiftNotebook;
 
public record GetDataForShiftNoteBookCommand(StoreRequestModel Request) : ICommand<Result>;

public class GetDataReportHandler(SmartDataServices dataServices, VoucherSgasDbContext dbContext) : ICommandHandler<GetDataForShiftNoteBookCommand, Result>
{
    public async Task<Result> Handle(GetDataForShiftNoteBookCommand request, CancellationToken cancellationToken)
    {

        //[dbo].[GetDataForShiftNotebook] 'DataShift','CHHN',888,'1561', '08/01/2023' ,'KHN','' ,'SmartSoftware'  
        ArgumentNullException.ThrowIfNull(request);
        //ALTER PROCEDURE [dbo].[GetDataForShiftNotebook] @Parameter nvarchar(50),@UserCode nvarchar(20),@CodeUnit int,@AccountSymbol nvarchar(20) , @Date nvarchar(10) ,@WareHouseCode nvarchar(255),@ProductCode nvarchar(512) ,@SmartSoftware nvarchar(50) 
        // 
        string queryStr =
            $@"exec {request.Request.StoreName} '{request.Request.Parameter}', '{request.Request.UserCode}', '{request.Request.CodeUnit}',   '{request.Request.AccountSymbol}', '{request.Request.Date}', '{request.Request.WareHouseCode}',  '{request.Request.ProductCode}', '{request.Request.SmartSoftware}'";

        var result = await dataServices.GetListObject<object>(queryStr, dbContext.Database.GetConnectionString()!, cancellationToken)
            .ConfigureAwait(false);

        return Result.Success(result);
    }
}