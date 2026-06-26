using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Web;
using Catalog.Base.Entities;
using Microsoft.Data.SqlClient;

namespace Catalog.Base.Features;
public class DapperApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Catalog");

        var api = vApi.MapGroup("product-plan").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("view", GetPrdPlan)
           .WithName("ListPrdPlan")
           .WithTags("Catalogs");

    }
    private async Task<IResult> GetPrdPlan([AsParameters] CatalogService service, ICurrentUser currentUser)
    {
        var cs = service.DbContext.Database.GetDbConnection().ConnectionString;
        string sql = $@"SELECT ObjectCode ,ObjectName,NumberOfVouchers,VoucherDate,IdVouchers,ListColumn FROM SmartDataProductionPlan where CodeUnit={currentUser.CodeUnit} group by  ObjectCode ,ObjectName,NumberOfVouchers,VoucherDate,IdVouchers,ListColumn";

        await using var con = new SqlConnection(cs);
        var rows = (await con.QueryAsync<SmartDataProductionPlan>(sql)).ToList();

        var result = new ApiResponse<List<SmartDataProductionPlan>>
        {
            Status = new StatusResponse
            {
                Code = 200,
                Desc = "Successfully"
            },
            Data = rows
        };

        return Results.Ok(result);
    }
}
