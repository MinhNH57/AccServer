using BuildingBlocks.Response;
using Catalog.Base.GenarateCode.Models;
using Microsoft.Data.SqlClient;

namespace Catalog.Base.GenarateCode;

public class GenerateCodeApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Catalog");
        var api = vApi.MapGroup("generate-code/").HasApiVersion(1.0);

        api.RequireAuthorization();

        api.MapPost("get-code-catalog-group", GetCodeCatalogByGroupCode)
            .WithSummary("Lấy ra mã tự sinh cho khóa chính")
            .WithTags("GenerateCode");

        api.MapPost("group-code-catalog", GroupCodeCategory)
            .WithSummary("Gộp mã")
            .WithTags("GenerateCode");

    }

    private async Task<IResult> GroupCodeCategory(
        [AsParameters] CatalogService service,
        GroupCodeQuery query)
    {
        if (string.IsNullOrWhiteSpace(query.SourceCode) || string.IsNullOrWhiteSpace(query.DestinationCode))
            return Results.BadRequest();

        string sql = "EXEC dbo.SmartGroupCodeCatalog @Param1, @Param2, @Param3, @Param4, @Param5, @Param6, @Param7";
        var parameters = new[]
        {
            new SqlParameter("@Param1", query.Parameter ?? (object)DBNull.Value),
            new SqlParameter("@Param2", query.UserCode ?? (object)DBNull.Value),
            new SqlParameter("@Param3", query.CodeUnit ?? (object)DBNull.Value),
            new SqlParameter("@Param4", query.TableName ?? (object)DBNull.Value),
            new SqlParameter("@Param5", query.SourceCode ?? (object)DBNull.Value),
            new SqlParameter("@Param6", query.DestinationCode ?? (object)DBNull.Value),
            new SqlParameter("@Param7", query.IsDelete)
        };
        await service.DbContext.Database.ExecuteSqlRawAsync(sql, parameters);
        return Results.Ok();
    }

    private async Task<IResult> GetCodeCatalogByGroupCode(
        [AsParameters] CatalogService service,
        QueryCodeCatalogByGroup query)
    {
        var result = await service.DbContext.Database.SqlQueryRaw<SmartCode>(
            "SELECT dbo.GetCodeCatalogByGroupCodeWeb({0}, {1}, {2},{3}) as SmartCode",
            query.TableName, query.GroupCode, query.Length, query.Ct).ToListAsync().ConfigureAwait(false);

        // Chuyển đổi kết quả từ kiểu object sang kiểu string

        return Results.Ok(Result.Success(result.ToList()[0]));
    }
}