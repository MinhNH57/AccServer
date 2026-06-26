using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using FileHandle.Data;
using FileHandle.Models.Request;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace FileHandle.Services;

public interface IExcelSmartDataServices
{
    Task<Result> CreateExcelSmartData(CreateExcelSmartDataRequest request, CancellationToken token);
    Task<Result> CreateVoucherToExcel(CreateVoucherToExcelRequest request, CancellationToken token);
}
public class ExcelSmartDataServices(ICurrentUser currentUser, FileHandleDbContext context, SmartDataServices dataServices) : IExcelSmartDataServices
{
    public async Task<Result> CreateExcelSmartData(CreateExcelSmartDataRequest request, CancellationToken token)
    {
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        string numberImport = $"{request.DataType}-{timestamp}-{currentUser.CodeUnit}-{currentUser.CodeUser}";
        request.DataImport.ForEach(x =>
        {
            x.NumberImport = numberImport;
            x.CreateBy = currentUser.CodeUser;
            x.CreatedBy = currentUser.CodeUser;
            x.TypeData = request.DataType;
            x.CreateDate = DateTime.Now;
        });
        await context.ExcelSmartData.AddRangeAsync(request.DataImport, token);
        var count = await context.SaveChangesAsync(token).ConfigureAwait(false);
        if (count > 0)
        {
            return Result.Success(true);
        }
        return Result.Failure<string>(new Error("400", "Có lỗi trong quá trình cất giữ"));
    }

    public async Task<Result> CreateVoucherToExcel(CreateVoucherToExcelRequest request, CancellationToken token)
    {

        //ALTER PROCEDURE [dbo].[ImportFromExcel] @Parameter nvarchar(50),@Id nvarchar(70),@UserCode nvarchar(20),@CodeUnit int,@AccountSymbol nvarchar(70),@WarehoseCode nvarchar(50),@ProductCode nvarchar(150),@BeginDate nvarchar(10) ,@EndDate nvarchar(10),@SmartSoftware nvarchar (50)
        // 
        string queryStr =
            $@"exec {request.StoreName} '{request.Parameter}', '{request.Id}', '{currentUser.CodeUser}', '{currentUser.CodeUnit}',   '{request.AccountSymbol}', '{request.WarehoseCode}', '{request.ProductCode}','{request.BeginDate}', '{request.EndDate}', '{request.SmartSoftware}'";

        var result = await dataServices.GetListObject<object>(queryStr, context.Database.GetConnectionString()!, token)
            .ConfigureAwait(false);

        return Result.Success(result);
    }
}
