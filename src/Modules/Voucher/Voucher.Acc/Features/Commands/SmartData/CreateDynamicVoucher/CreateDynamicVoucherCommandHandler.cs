using System.Reflection;
using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.SmartData.CreateDynamicVoucher;

public class CreateDynamicVoucherCommandHandler(
    VoucherDbContext dbContext,
    IMultiTenantContextAccessor tenantContextAccessor,
    ICurrentUser currentUser)
    : ICommandHandler<CreateDynamicVoucherCommand, Result>
{
    TenantInfoCustomize? _tenant = tenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;

    public async Task<Result> Handle(CreateDynamicVoucherCommand request, CancellationToken cancellationToken)
    {
        string Id = "";
        foreach (var item in request.DynamicData)
        {
            var type = GetEntityType(item.EntityType);

            var listType = typeof(List<>).MakeGenericType(type);

            //Chuyển obj để tạo
            if (item.JsonDataCreate.Length > 5)
            {
                var entityCreate = JsonConvert.DeserializeObject(item.JsonDataCreate, listType) as IEnumerable<object>;

                if (entityCreate is not null)
                {
                    await dbContext.AddRangeEntities(entityCreate, CancellationToken.None);
                    foreach (dynamic obj in entityCreate)
                    {
                        if (obj != null)
                        {
                            try
                            {
                                Id = obj.Id.ToString();
                                Console.WriteLine(Id);
                            }
                            catch (RuntimeBinderException)
                            {
                                // ko có trường ID
                            }
                        }
                    }
                }
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

        return Result.Success(Id);
    }

    private async Task<bool> DeleteEntity(SmartDeleteDataQuery request)
    {
        ArgumentNullException.ThrowIfNull(request);
        var dataServices = new SmartDataServices();
        if (_tenant is null)
            return false;

        var obj = new
        {
            request.Parameter,
            request.TableName,
            request.KeyData,
            request.DataPlus,
            MaUser = currentUser.CodeUser,
            currentUser.CodeUnit,
        };
        var isSuccess = await dataServices.ExcuteNonQueryAsync(request.StoreName, _tenant.ConnectionString(), obj);

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