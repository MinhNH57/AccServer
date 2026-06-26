using System.Reflection;
using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Index.HPRtree;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.Sales.CreateSalesVoucher;

public class CreateSalesVoucherCommandHandler(
    VoucherDbContext dbContext,
    ICurrentUser currentUser)
    : ICommandHandler<CreateSalesVoucherCommand, Result<string>>
{

    public async Task<Result<string>> Handle(CreateSalesVoucherCommand request, CancellationToken cancellationToken)
    {

        string id = "";
        foreach (JObject jo in JArray.Parse(request.DynamicData[0].JsonDataCreate)) id = jo["Id"].ToString();
        foreach (var item in request.DynamicData)
        {
            var type = GetEntityType(item.EntityType);

            var listType = typeof(List<>).MakeGenericType(type);

            //Chuyển obj để tạo
            if (item.JsonDataCreate.Length > 5)
            {
                var jarr = JArray.Parse(item.JsonDataCreate);
                foreach (JObject jo in jarr) jo["CodeUnit"] = currentUser.CodeUnit ;
                var creates = (IEnumerable<object>)jarr.ToObject(listType)!;
                await dbContext.AddRangeEntities(creates, CancellationToken.None);
                //var entityCreate = JsonConvert.DeserializeObject(item.JsonDataCreate, listType) as IEnumerable<object>;

                //if (entityCreate is not null)
                //{
                //    await dbContext.AddRangeEntities(entityCreate, CancellationToken.None);
                //}
            }
            //Chuyển obj để update
            if (item.JsonDataUpdate.Length > 5)
            {
                if (JsonConvert.DeserializeObject(item.JsonDataUpdate, listType) is IEnumerable<object> entityUpdate)
                {
                    dbContext.UpdateRangeEntities(entityUpdate, CancellationToken.None);
                }
            }
            foreach (var deleteQuery in item.DataRemove.Select(itemRemove => new SmartDeleteDataQuery("DeleteData", item.EntityType, "", itemRemove, ""
                     )))
            {
                await DeleteEntity(deleteQuery);
            }
        }
        var rsl = await dbContext.SaveChangesAsync(cancellationToken) > 0;

        return Result.Success<string>(id);
    }

    private async Task<bool> DeleteEntity(SmartDeleteDataQuery request)
    {
        ArgumentNullException.ThrowIfNull(request);
        var dataServices = new SmartDataServices();
       
        var obj = new
        {
            request.Parameter,
            request.TableName,
            request.KeyData,
            request.DataPlus,
            MaUser = currentUser.CodeUser,
            currentUser.CodeUnit,
        };
        var isSuccess = await dataServices.ExcuteNonQueryAsync(request.StoreName, dbContext.Database.GetConnectionString(), obj);

        if (!isSuccess)
        {
            //Log.Error("User: '{userName}' delete failed data with type: '{entityType}' content: '{json}'.",
            //    currentUser.CodeUser, request.Parameter, JsonConvert.SerializeObject(request));
            return false;
        }

        //await redisCache.RemoveByPatternAsync(GetCatalogCacheKey(request.TableName));
        //Log.Information("User: '{userName}' deleted data with type: '{entityType}' content: '{json}'.",
        //    currentUser.CodeUser, request.Parameter, JsonConvert.SerializeObject(request));
        return true;

    }

    private Type GetEntityType(string entityType)
    {
        // Tải assembly chứa các model
        var assName = Assembly.GetAssembly(typeof(VoucherRoot))?.FullName;
        var assembly = Assembly.Load(assName); // Tên assembly chứa các model

        // Duyệt qua tất cả các Type trong assembly để tìm Type khớp với tên entityType
        var type = assembly.GetTypes().FirstOrDefault(t => t.Name == entityType);
        if (type == null)
        {
            throw new InvalidOperationException($"Type '{entityType}' không tồn tại trong assembly '{assName}'.");
        }

        return type;
    }
}