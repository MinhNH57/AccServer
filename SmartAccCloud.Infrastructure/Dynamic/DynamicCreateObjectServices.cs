using System.Reflection;
using AutoMapper;
using Finbuckle.MultiTenant.Abstractions;
using Newtonsoft.Json;
using Serilog;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.DynamicModel;
using SmartAccCloud.Application.Models.GetDatas;
using SmartAccCloud.Application.Models.Users;
using SmartAccCloud.Application.Services.Dynamic;
using SmartAccCloud.Infrastructure.Caching;
using SmartAccCloud.Infrastructure.Persistence.Dapper;

namespace SmartAccCloud.Infrastructure.Dynamic;

public class DynamicCreateObjectServices(
    IApplicationDbContext context,
    IMapper mapper,
    ICurrentUser currentUser,
    IDistributedCache cache,
    RedisCacheService redisCache,
    IMultiTenantContextAccessor tenantContextAccessor) : IDynamicCreateObjectServices
{
    TenantInfoCustomize? _tenant = tenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;

    public async Task<bool> CreateEntityFromJson(string entityType, string jsonData)
    {
        ArgumentException.ThrowIfNullOrEmpty(entityType);
        ArgumentException.ThrowIfNullOrEmpty(jsonData);

        var type = GetEntityType(entityType);

        // Deserialize JSON thành đối tượng động
        var entity = JsonConvert.DeserializeObject(jsonData, type);
        if (entity == null)
        {
            return false;
        }

        await context.AddEntity(entity, CancellationToken.None);

        var rsl = await context.SaveChangesAsync() > 0;
        if (!rsl)
        {
            Log.Information("User: '{userName}' created data with type: '{entityType}' content: '{json}'.", currentUser.CodeUser, entityType, jsonData);
            return false;
        }
        await redisCache.RemoveByPatternAsync(GetCatalogCacheKey(entityType));
        Log.Information("User: '{userName}' created data with type: '{entityType}' content: '{json}'.", currentUser.CodeUser, entityType, jsonData);
        return true;
    }

    public async Task<bool> CreateEntitiesFromJson(string entityType, string jsonData)
    {
        ArgumentException.ThrowIfNullOrEmpty(entityType);
        ArgumentException.ThrowIfNullOrEmpty(jsonData);

        var type = GetEntityType(entityType);

        var listType = typeof(List<>).MakeGenericType(type);

        // Deserialize JSON thành đối tượng động
        var entity = JsonConvert.DeserializeObject(jsonData, listType) as IEnumerable<object>;
        if (entity == null)
        {
            Log.Error("User: '{userName}' create failed data with type: '{entityType}' content: '{json}'.",
                currentUser.CodeUser, entityType, jsonData);
            return false;
        }

        await context.AddRangeEntities(entity, CancellationToken.None);
        var rsl = await context.SaveChangesAsync() > 0;
        if (!rsl)
        {
            Log.Error("User: '{userName}' create failed data with type: '{entityType}' content: '{json}'.",
                currentUser.CodeUser, entityType, jsonData);
            return false;
        }
        await redisCache.RemoveByPatternAsync(GetCatalogCacheKey(entityType));
        Log.Information("User: '{userName}' created data with type: '{entityType}' content: '{json}'.",
            currentUser.CodeUser, entityType, jsonData);
        // await cache.RemoveAsync(entityType);
        return true;
    }

    public async Task<bool> UpdateEntity(UpdateObjectDynamicRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var type = GetEntityType(request.EntityType);
        //var json = JsonConvert.SerializeObject(request.JsonData);
        var json = request.JsonData.ToString();
        var entity = JsonConvert.DeserializeObject(json, type);

        if (entity == null)
        {
            Log.Error("User: '{userName}' edit failed data with type: '{entityType}' content: '{json}'.",
                currentUser.CodeUser, request.EntityType, request.JsonData);
            return false;
        }
        context.UpdateEntity(entity);
        await context.SaveChangesAsync();
        await redisCache.RemoveByPatternAsync(GetCatalogCacheKey(request.EntityType));
        //await cache.RemoveAsync(GetCatalogCacheKey(request.EntityType));
        Log.Information("User: '{userName}' Edited data with type: '{entityType}' content: '{json}'.",
            currentUser.CodeUser, request.EntityType, request.JsonData);
        return true;
    }

    /// <summary>
    /// Thêm sửa xóa động
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAndRemoveEntity(UpdateAndRemoveObjectDynamicRequest request)
    {
        ArgumentException.ThrowIfNullOrEmpty(request.EntityType);
        var type = GetEntityType(request.EntityType);

        var listType = typeof(List<>).MakeGenericType(type);

        // Deserialize JSON thành đối tượng động
        var entityUpdate = JsonConvert.DeserializeObject(request.JsonDataUpdate, listType) as IEnumerable<object>;
        var entityCreate = JsonConvert.DeserializeObject(request.JsonDataCreate, listType) as IEnumerable<object>;

        if (entityCreate.Any())
        {
            context.AddRangeEntities(entityCreate, CancellationToken.None);
        }

        foreach (var itemUpdate in entityUpdate)
        {
            context.UpdateEntity(itemUpdate);
        }

        foreach (var deleteQuery in request.DataRemove.Select(itemRemove => new SmartDeleteDataQuery("DeleteData", request.EntityType, "", itemRemove, ""
                 )))
        {
            await DeleteEntity(deleteQuery);
        }
        var rsl = await context.SaveChangesAsync() > 0;
        if (!rsl)
        {
            Log.Error("User: '{userName}' edit failed data with type: '{entityType}' content: '{json}'.",
                currentUser.CodeUser, request.EntityType, JsonConvert.SerializeObject(request));
            return false;
        }
        await redisCache.RemoveByPatternAsync(GetCatalogCacheKey(request.EntityType));
        Log.Information("User: '{userName}' edited data with type: '{entityType}' content: '{json}'.",
            currentUser.CodeUser, request.EntityType, JsonConvert.SerializeObject(request));
        // await cache.RemoveAsync(entityType);
        return true;
    }

    public async Task<bool> DeleteEntity(SmartDeleteDataQuery request)
    {
        ArgumentNullException.ThrowIfNull(request);
        IDataServices dataServices = new SmartDataServices();
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
            Log.Error("User: '{userName}' delete failed data with type: '{entityType}' content: '{json}'.",
                currentUser.CodeUser, request.Parameter, JsonConvert.SerializeObject(request));
            return false;
        }

        await redisCache.RemoveByPatternAsync(GetCatalogCacheKey(request.TableName));
        Log.Information("User: '{userName}' deleted data with type: '{entityType}' content: '{json}'.",
            currentUser.CodeUser, request.Parameter, JsonConvert.SerializeObject(request));
        return true;
    }

    public async Task<bool> DynamicAddListData(List<DynamicListDataModel> request)
    {
        foreach (var item in request)
        {
            var type = GetEntityType(item.EntityType);

            var listType = typeof(List<>).MakeGenericType(type);

            //Chuyển obj để tạo
            if (item.JsonDataCreate.Length > 5)
            {
                var entityCreate = JsonConvert.DeserializeObject(item.JsonDataCreate, listType) as IEnumerable<object>;

                if (entityCreate is not null)
                {
                    await context.AddRangeEntities(entityCreate, CancellationToken.None);
                }
            }
            //Chuyển obj để update
            if (item.JsonDataUpdate.Length > 5)
            {
                if (JsonConvert.DeserializeObject(item.JsonDataUpdate, listType) is IEnumerable<object> entityUpdate)
                {
                    context.UpdateRangeEntities(entityUpdate, CancellationToken.None);
                }
            }
            foreach (var deleteQuery in item.DataRemove.Select(itemRemove => new SmartDeleteDataQuery("DeleteData", item.EntityType, "", itemRemove, ""
                     )))
            {
                await DeleteEntity(deleteQuery);
            }
        }
        var rsl = await context.SaveChangesAsync() > 0;
        await redisCache.RemoveByPatternAsync(GetCatalogCacheKey(request[0].EntityType));
        return true;
    }

    public async Task<IEnumerable<object>> GetDynamicEnitity(SmartGetDataQuery request)
    {
        IDataServices dataServices = new SmartDataServices();
        TenantInfoCustomize tenant = (tenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize)!;
        if (request.RequestType == "Detail")
        {
            var obj = new
            {
                request.Parameter,
                request.TableName,
                request.TypeDocument,
                request.FirstOrLast,
                currentUser.CodeUser,
                currentUser.CodeUnit,
                request.Condition,
            };
            //Console.WriteLine($"{request.TableName} get from DB");
            return await dataServices.GetListObject<object>(request.StoreName, tenant.ConnectionString(), obj);
        }

        var data = await cache.GetOrCreateAsync($"KT:catalog:{tenant.Identifier}:{request.TableName}:{request.TypeDocument}", async () =>
        {
            var obj = new
            {
                request.Parameter,
                request.TableName,
                request.TypeDocument,
                request.FirstOrLast,
                currentUser.CodeUser,
                currentUser.CodeUnit,
                request.Condition,
            };
            Console.WriteLine($"{request.TableName} get from DB");
            return await dataServices.GetListObject<object>(request.StoreName, tenant.ConnectionString(), obj);
        }, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cache.RandomExpireTime(15, 30)),
            SlidingExpiration = TimeSpan.FromMinutes(5)
        });

        var type = GetEntityType(request.TableName);

        var jsonData = JsonConvert.SerializeObject(data);
        var listType = typeof(List<>).MakeGenericType(type);
        var da = JsonConvert.DeserializeObject(jsonData, listType) as IEnumerable<object>;
        //var res = JsonConvert.SerializeObject(data);
        Console.WriteLine($"{request.TableName} get from cache");

        return da;

    }
    /// <summary>
    /// Thêm động các thực thể có bảng con
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> DynamicAddParentAndChild(DynamicRequestParentAdd request)
    {
        var resultAdd = await CreateEntityFromJson(request.ParentRequest.EntityType, request.ParentRequest.JsonData).ConfigureAwait(false);
        if (resultAdd)
        {
            if (request.ContentRequest.JsonData == string.Empty || request.ContentRequest.JsonData == "[]") return true;
            var resultUpdate = await CreateEntitiesFromJson(request.ContentRequest.EntityType, request.ContentRequest.JsonData).ConfigureAwait(false);

            if (resultUpdate)
                return true;
        }

        return false;
    }

    private Type GetEntityType(string entityType)
    {
        // Tải assembly chứa các model
        var assembly = Assembly.Load("SmartAccCloud.Domain"); // Tên assembly chứa các model

        // Duyệt qua tất cả các Type trong assembly để tìm Type khớp với tên entityType
        var type = assembly.GetTypes().FirstOrDefault(t => t.Name == entityType);
        if (type == null)
        {
            throw new InvalidOperationException($"Type '{entityType}' không tồn tại trong assembly 'SmartAccCloud.Domain'.");
        }

        return type;
    }
    private string GetCatalogCacheKey(string id)
    {
        return $"KT:catalog:{_tenant.Identifier}:{id}";
    }
}


