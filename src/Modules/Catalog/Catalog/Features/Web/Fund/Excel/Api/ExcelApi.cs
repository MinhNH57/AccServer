using Carter;
using Catalog.Base;
using Catalog.Features.Web.Fund.Excel.Command;

namespace Catalog.Features.Web.Fund.Excel.Api;

public class ExcelApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Excel");
        var api = vApi.MapGroup("fund/excel/").HasApiVersion(1.0).RequireAuthorization();
    }
}
