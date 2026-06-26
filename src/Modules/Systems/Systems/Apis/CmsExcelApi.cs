using BuildingBlocks.Web;
using Dapper;
using Serilog;
using System.Data;
using Systems.Infrastructure.Entities;
using Systems.Models.ExcelRequest;

namespace Systems.Apis;

public class CmsExcelApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        var vApi = app.NewVersionedApi("ExcelConfigs");
        var api = vApi.MapGroup("config-excel-system/").HasApiVersion(1.0);
        api.RequireAuthorization();


        api.MapPost("/settings/excel/generate-properties", GenerateExcelProperties)
            .WithName("GenerateExcelProperties")
            .WithSummary("Tạo thuộc tính cấu hình excel config.")
            .WithDescription("Tạo thuộc tính của một cấu hình excel config.")
            .WithTags("ExcelConfigs");

        api.MapPut("/settings/excel/update-excel-config", UpdateExcelConfigs)
            .WithName("update-excel-config")
            .WithSummary("Cập nhật thông tin excel config")
            .WithDescription("Cập nhật thông tin excel config")
            .WithTags("ExcelConfigs");

        api.MapGet("/settings/excel/get-excel-config/{dataType}", GetExcelConfig)
            .WithName("get-excel-config")
            .WithSummary("Lấy thông tin excel config")
            .WithDescription("Lấy thông tin excel config")
            .WithTags("ExcelConfigs");

    }

    private static async Task<IResult> GetExcelConfig(
        [AsParameters] SystemService services,
        string dataType)
    {
        var componentSetting = await services.DbContext.SmartMapColumnExcel
            .Where(x => x.DataType == dataType).OrderBy(x=>x.ColumnIndex).AsNoTracking().ToListAsync().ConfigureAwait(false);

        if (!componentSetting.Any())
        {
            return Results.Ok(Result.Success(new List <SmartMapColumnExcel>(), new("404", "Không tồn tại cấu hình excel này")));
        }

        return Results.Ok(Result.Success(componentSetting));
    }
    public static async Task<IResult> GenerateExcelProperties(
        [AsParameters] SystemService services,
      ExcelDataParameter parameter)
    {
        var componentSetting = await services.DbContext.SmartMapColumnExcel
            .Where(x => x.DataType == parameter.DataType).AsNoTracking().ToListAsync().ConfigureAwait(false);

        if (!componentSetting.Any())
        {
            var connection = services.DbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            await connection.ExecuteAsync(
                @"EXEC [dbo].[CreateMapColumnExcel] 
                @Parameter, 
                @DataType, 
                @UserCode, 
                @CodeUnit, 
                @TableNameDataExcel",
                new
                {
                    Parameter = parameter.Parameter,
                    DataType = parameter.DataType,
                    UserCode = parameter.UserCode,
                    CodeUnit = parameter.CodeUnit,
                    TableNameDataExcel = parameter.TableNameDataExcel
                });
        }
        componentSetting = await services.DbContext.SmartMapColumnExcel
            .Where(x => x.DataType == parameter.DataType).ToListAsync().ConfigureAwait(false);

        return Results.Ok(Result.Success(componentSetting));

    }
    public static async Task<IResult> UpdateExcelConfigs(
        [AsParameters] SystemService service,
        ExcelDataUpdateRequest request,
        ICurrentUser currentUser,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        var lstConfigEdit = await service.DbContext.SmartMapColumnExcel.AsNoTracking()
            .Where(x => x.DataType == request.DataType)
            .ToListAsync(cancellationToken: token);
        if (lstConfigEdit.Any())
        { 

            var lstUpdate = request.ListConfig
                .Where(c => lstConfigEdit.Any(r => r.Id == c.Id))
                .ToList();

            lstUpdate.ForEach(x =>
            {
                var updated = request.ListConfig.FirstOrDefault(y => y.ColumNameTable == x.ColumNameTable);
                if (updated != null)
                {
                    x.ColumnIndex = updated.ColumnIndex;
                    x.ColumnNameExcel = updated.ColumnNameExcel;
                    x.DisplayName = updated.DisplayName;
                    x.IsNumber = updated.IsNumber;
                    x.IsDate = updated.IsDate;
                    x.IsMulty = updated.IsMulty;
                    x.IsTrueFalse = updated.IsTrueFalse;
                    x.ColumnDataType = updated.ColumnDataType;
                    x.IsActive = updated.IsActive;
                }
            });

            var lstCreate = request.ListConfig!
                .Where(x => lstConfigEdit.All(y => y.Id != x.Id))
                .ToList();

            var lstRemove = lstConfigEdit
                .Where(y => request.ListConfig!.All(x => x.Id != y.Id))
                .ToList();


            service.DbContext.SmartMapColumnExcel.UpdateRange(lstUpdate);
            service.DbContext.SmartMapColumnExcel.RemoveRange(lstRemove);
            await service.DbContext.SmartMapColumnExcel.AddRangeAsync(lstCreate, token);
            var count = await service.DbContext.SaveChangesAsync(token);
            if (count > 1)
            {
                Log.Information($"[SmartMapColumnExce] User: {currentUser.CodeUser} has edited the data in the table SmartMapColumnExce:{request.DataType}");
                return Results.Ok(Result.Success(true));
            }
        }
        return Results.BadRequest(Result.Failure(new Error("400", "Có lỗi khi cất giữ khoá sổ.")));

    }

}
