using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Pagination.Version1;
using Carter;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Features.Commands.GetDataInheritVoucherType;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Commands.GetDataInventoryForOrderBuy;
public class DataInventoryForOrderBuy : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapGet("dataInventoryForOrderBuy/{numberOfvoucher}", GetDataInventoryForOrderBuy)
            .WithName("Danh sách đề nghị cung ứng còn tồn")
            .WithTags("Vouchers");
    }

    private async Task<IResult> GetDataInventoryForOrderBuy(VoucherDbContext dbContext, string numberOfvoucher)
    {
        var cs = dbContext.Database.GetConnectionString();
        string sql = $@"select head.Id,Cont.IdSource,Cont.IdContents,head.NumberOfVouchers,head.RecordDate,head.ObjectCode,head.ObjectName,cont.Specifications,cont.UnitPackage,Cont.ConversionFactor,cont.PackageQuantity,cont.WarehoseCode,cont.WarehoseName,cont.CommodityCode,cont.CommodityName,cont.UnitPcs,cont.Quantity,Inven.QuantityOfInventory from SalesSmartData head inner join SalesSmartContentsData cont on head.Id=Cont.IdContents
                    inner join [DataInventoryForOrderBuy] Inven on Cont.IdSource=Inven.IdSource 
                    and head.DataType='Request' where NumberOfVouchers='{numberOfvoucher}' and Inven.QuantityOfInventory>0";

        await using var con = new SqlConnection(cs);
        var rows = (await con.QueryAsync<object>(sql)).ToList();

        var result = new ApiResponse<List<object>>
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
