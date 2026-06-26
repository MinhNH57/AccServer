using BuildingBlocks.Pagination.Version2;
using BuildingBlocks.Response;
using Catalog.Base.Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.NetworkInformation;
using System.Reflection;
using PaginationRequest = BuildingBlocks.Pagination.Version1.PaginationRequest;
using Result = BuildingBlocks.Response.Result;

namespace Catalog.Base.Features;

public class CatalogApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Catalog");

        var api = vApi.MapGroup("catalog/").HasApiVersion(1.0).RequireAuthorization();

        // Routes for querying object.

        api.MapGet("{catalog}", FindAll)
            .WithName("ListItems")
            .WithSummary("Danh sách các mục trong danh mục.")
            .WithDescription("Nhận danh sách các mục được phân trang trong danh mục.")
            .WithTags("Catalogs");
        api.MapGet("view/{typeSize}", GetViewAsync)
            .WithName("ListView")
            .WithTags("Catalogs");
        api.MapGet("v2/{catalog}", FindAllv2)
            .WithName("GetCatalogV2")
            .WithSummary("Danh sách các mục trong danh mục.")
            .WithDescription("Nhận danh sách các mục được phân trang trong danh mục.")
            .WithTags("Catalogs");

        api.MapGet("find-cache/{catalog}", FindAllCache)
            .WithName("ListItemsCache")
            .WithSummary("Danh sách các mục trong danh mục.")
            .WithDescription("Nhận danh sách các mục được phân trang trong danh mục.")
            .WithTags("Catalogs");

        api.MapPost("get-catalog", GetDynamicEnitity)
            .WithName("get-catalog")
            .WithSummary("Danh sách các mục trong danh mục.")
            .WithDescription("Nhận danh sách các mục được phân trang trong danh mục.")
            .WithTags("Catalogs");

        // Routes for modifying object.

        api.MapPost("{catalog}", Create)
            .WithName("CreateItem")
            .WithSummary("Tạo một mục trong danh mục.")
            .WithDescription("Tạo một mục mới trong danh mục.")
            .WithTags("Catalogs");

        api.MapPut("{catalog}", Update)
            .WithName("UpdateItem")
            .WithSummary("Tạo hoặc thay thế một mục trong danh mục.")
            .WithDescription("Tạo hoặc thay thế một mục trong danh mục.")
            .WithTags("Catalogs");

        api.MapDelete("{catalog}/{id:guid}", Delete)
            .WithName("DeleteItem")
            .WithSummary("Xóa mục trong danh mục.")
            .WithDescription("Xóa một mục chỉ định trong danh mục.")
            .WithTags("Catalogs");


        api.MapPost("dynamic-create-object", CreateEntityFromJson)
            .WithName("dynamic-create-object")
            .WithSummary("Thêm động dữ liệu danh mục")
            .WithTags("Catalogs");
        api.MapPost("dynamic-create-listobject", CreateEntitiesFromJson)
            .WithName("dynamic-create-listobject")
            .WithSummary("Thêm động list danh mục")
            .WithTags("Catalogs");

        api.MapPost("dynamic-add-list-data", DynamicAddListData)
            .WithName("dynamic-add-list-data")
            .WithSummary("Thêm động dữ liệu list danh mục")
            .WithTags("Catalogs");

        api.MapPut("dynamic-update-object", UpdateDynamicObject)
            .WithName("dynamic-update-object")
            .WithSummary("Cập nhật động dữ liệu  danh mục")
            .WithTags("Catalogs");


        api.MapPut("delete-catalog", DeleteEntity)
            .WithName("delete-catalog")
            .WithSummary("Xoá danh mục động theo tên bảng và khoá chính")
            .WithTags("Catalogs");

        api.MapPost("check-exits", CheckDynamicEnitity)
            .WithName("check-exits")
            .WithSummary("Check xem model có tồn tại không.")
            .WithDescription("Check xem model có tồn tại trong hệ thống qua các tham số truyền vào không.")
            .WithTags("Catalogs");
    }

    private async Task<IResult> FindAllCache(
        [AsParameters] CatalogService service,
        [AsParameters] FilteringRequest filtering,
        [AsParameters] SortRequest sorting,
        [AsParameters] PaginationRequest pagination,
        string catalog, string? typeView)
    {
        var query = new GetDynamicCatalogQuery(filtering, sorting, pagination, catalog, typeView);
        var result = await service.Mediator.Send(query);
        return Results.Ok(result);
    }


    private async Task<IResult> DynamicAddListData(
        [AsParameters] CatalogService service,
        List<DynamicListDataModel> request)
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
                    //await service.DbContext.AddRangeEntities(entityCreate, CancellationToken.None);
                    await service.DbContext.AddRangeEntities(entityCreate, CancellationToken.None);
                }
            }
            //Chuyển obj để update
            if (item.JsonDataUpdate.Length > 5)
            {
                if (JsonConvert.DeserializeObject(item.JsonDataUpdate, listType) is IEnumerable<object> entityUpdate)
                {
                    service.DbContext.UpdateRangeEntities(entityUpdate, CancellationToken.None);
                }
            }
            foreach (var deleteQuery in item.DataRemove.Select(itemRemove => new SmartDeleteDataQuery("DeleteData", item.EntityType, "", itemRemove, ""
                     )))
            {
                await DeleteEntity(service, deleteQuery);
            }
        }

        var rsl = await service.DbContext.SaveChangesAsync() > 0;

        await service.CacheService
            .RemoveByPatternAsync(GetCatalogCacheKey(service.CurrentUser.TenantId!, request[0].EntityType));
        return Results.Ok("Entity create successfully.");
    }

    private async Task<IResult> UpdateDynamicObject(
        [AsParameters] CatalogService service,
        UpdateObjectDynamicRequest request)
    {
        var type = GetEntityType(request.EntityType);
        //var json = JsonConvert.SerializeObject(request.JsonData);
        var json = request.JsonData.ToString();
        var entity = JsonConvert.DeserializeObject(json, type);

        if (entity == null)
        {
            service.Logger.LogError("User: '{userName}' edit failed data with type: '{entityType}' content: '{json}'.",
                service.CurrentUser.CodeUser, request.EntityType, request.JsonData);
            return Results.Ok("Entity update fail.");
        }

        service.DbContext.UpdateEntity(entity);
        await service.DbContext.SaveChangesAsync();

        await service.CacheService.RemoveByPatternAsync(GetCatalogCacheKey(service.CurrentUser.TenantId!, request.EntityType));

        if (request.EntityType == "ReportConfigPages")
        {
            await service.CacheService.RemoveByPatternAsync(GetMenuCacheKey(service.CurrentUser.TenantId!, request.EntityType));

        }

        service.Logger.LogInformation("User: '{userName}' Edited data with type: '{entityType}' content: '{json}'.",
            service.CurrentUser.CodeUser, request.EntityType, request.JsonData);
        return Results.Ok("Entity updated successfully.");
    }

    private async Task<IResult> CreateEntityFromJson(
        [AsParameters] CatalogService service,
       CreateObjectDynamicRequest request)
    {
        //TODO: map prop CodeUnit vào nếu có
        var type = GetEntityType(request.EntityType);

        // Deserialize JSON thành đối tượng động
        var entity = JsonConvert.DeserializeObject(request.JsonData, type);

        if (entity == null)
        {
            return Results.BadRequest("Failed to create entity.");
        }
        var lstProp = type.GetProperties();
        foreach (var prop in lstProp)
        {
            if (prop.Name == "CodeUnit")
            {
                if (await service.DbContext.CatalogTableCommon
                        .Select(c => new { c.NameTable, c.IsCommon })
                        .FirstOrDefaultAsync(c => c.NameTable == request.EntityType) is { IsCommon: false })
                {
                    // Check Danh mục có phải là dùng chung (CatalogTableCommon) k nếu k thì map với CodeUnit
                    prop.SetValue(entity, service.CurrentUser.CodeUnit);
                }
                break;
            }
        }
        await service.DbContext.AddEntity(entity, CancellationToken.None);

        var rsl = await service.DbContext.SaveChangesAsync() > 0;
        if (!rsl)
        {
            //Log.Information("User: '{userName}' created data with type: '{entityType}' content: '{json}'.", currentUser.CodeUser, entityType, jsonData);
            return Results.BadRequest("Failed to create entity.");
        }

        await service.CacheService
            .RemoveByPatternAsync(GetCatalogCacheKey(service.CurrentUser.TenantId!, request.EntityType));
        if (request.EntityType == "ReportConfigPages")
        {
            await service.CacheService.RemoveByPatternAsync(GetMenuCacheKey(service.CurrentUser.TenantId!, request.EntityType));

        }
        //Log.Information("User: '{userName}' created data with type: '{entityType}' content: '{json}'.", currentUser.CodeUser, entityType, jsonData);

        return Results.Ok("Entity created successfully.");
    }
    private async Task<IResult> CreateEntitiesFromJson(
     [AsParameters] CatalogService service,
     CreateObjectDynamicRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.JsonData))
            return Results.BadRequest("No data provided.");

        var type = GetEntityType(request.EntityType);
        //Chuyển chuỗi Json thành List<Object>
        var entities = JsonConvert.DeserializeObject(
            request.JsonData,
            typeof(List<>).MakeGenericType(type)
        ) as System.Collections.IEnumerable;

        if (entities == null)
            return Results.BadRequest("Invalid JSON data.");

        foreach (var entity in entities)
        {
            var prop = type.GetProperty("CodeUnit");
            if (prop != null)
            {
                if (await service.DbContext.CatalogTableCommon
                    .Select(c => new { c.NameTable, c.IsCommon })
                    .FirstOrDefaultAsync(c => c.NameTable == request.EntityType) is { IsCommon: false })
                {
                    prop.SetValue(entity, service.CurrentUser.CodeUnit);
                }
            }

            await service.DbContext.AddEntity(entity, CancellationToken.None);
        }

        var rsl = await service.DbContext.SaveChangesAsync() > 0;
        if (!rsl)
            return Results.BadRequest("Failed to create entities.");

        await service.CacheService
            .RemoveByPatternAsync(GetCatalogCacheKey(service.CurrentUser.TenantId!, request.EntityType));

        return Results.Ok("Entities created successfully.");
    }

    //[AllowAnonymous]
    public async Task<IEnumerable<object>> GetDynamicEnitity(
        [AsParameters] CatalogService service,
        SmartGetDataQuery request)
    {
        //IDataServices dataServices = new SmartDataServices();
        var tenant = (service.TenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize)!;
        if (request.RequestType == "Detail")
        {
            var obj = new
            {
                request.Parameter,
                request.TableName,
                request.TypeDocument,
                request.FirstOrLast,
                service.CurrentUser.CodeUser,
                service.CurrentUser.CodeUnit,
                request.Condition,
            };
            //Console.WriteLine($"{request.TableName} get from DB");
            return await service.SmartDataServices.GetListObject<object>(request.StoreName, tenant.ConnectionString(), obj);
        }

        var data = await service.Cache.GetOrCreateAsync($"KT:{tenant.Identifier}:catalog:{request.TableName}:{request.TypeDocument}", async () =>
        {
            var obj = new
            {
                request.Parameter,
                request.TableName,
                request.TypeDocument,
                request.FirstOrLast,
                service.CurrentUser.CodeUser,
                service.CurrentUser.CodeUnit,
                request.Condition,
            };
            Console.WriteLine($"{request.TableName} get from DB");
            return await service.SmartDataServices.GetListObject<object>(request.StoreName, tenant.ConnectionString(), obj);
        }, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(service.Cache.RandomExpireTime(15, 30)),
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

    public async Task<bool> CheckDynamicEnitity(
      [AsParameters] CatalogService service,
      DynamicCheckExist request)
    {
        var type = GetEntityType(request.TableName);

        var connection = service.DbContext.Database.GetDbConnection();
        await connection.OpenAsync();

        // Xây dựng câu lệnh SQL động
        var commandText = $"SELECT COUNT(*) FROM {request.TableName} WHERE {request.Column} =  N'{request.Value}' ";

        await using var command = connection.CreateCommand();
        command.CommandText = commandText;
        // Nếu > 0 thì đối tượng đã tồn tại trả về true.
        var result = await command.ExecuteScalarAsync();
        return result is > 0;

    }

    private Type GetEntityType(string entityType)
    {
        // Tải assembly chứa các model
        var assemblyData = Assembly.Load("Catalog.Base"); // Tên assembly chứa các model
        // Duyệt qua tất cả các Type trong assembly để tìm Type khớp với tên entityType
        var typeData = assemblyData.GetTypes().FirstOrDefault(t => t.Name == entityType);
        if (typeData is not null) return typeData;


        var assemblyFund = Assembly.Load("Catalog.Fund");
        var typeFund = assemblyFund.GetTypes().FirstOrDefault(t => t.Name == entityType);
        if (typeFund is not null) return typeFund;

        var assemblyHRM = Assembly.Load("Catalog.HRM");
        var typeHrm = assemblyHRM.GetTypes().FirstOrDefault(t => t.Name == entityType);
        if (typeHrm is not null) return typeHrm;


        throw new InvalidOperationException($"Type '{entityType}' không tồn tại trong assembly 'Catalog.Base , Catalog.Hrm , Catalog.Fund'.");

    }
    private async Task<IResult> FindAllv2(
        [AsParameters] CatalogService service,
        [AsParameters] FilteringRequest filtering,
        [AsParameters] SortRequest sorting,
        [AsParameters] PaginationRequest pagination,
        string catalog, string? typeView, string? searchFields)
    {
        long totalItems;
        IEnumerable<object> data;
        var page = pagination?.Page > 0 ? (int)pagination.Page : 1;
        //var pageSize = pagination?.PageSize > 1 ? (pagination.PageSize > 250 ? 250 : (int)pagination.PageSize) : 50;  
        var pageSize = pagination?.PageSize ?? 1000;
        var filter = filtering.Filter.Select(x =>
        {
            if (x.Split(":").Length == 3) return new Filter { Field = x.Split(":")[0], Operator = x.Split(":")[1], Value = x.Split(":")[2] };
            else if (x.Split(":").Length > 3) return new Filter { Field = x.Split(":")[0], Operator = x.Split(":")[1], Value = x.Split(":", 3)[2] };
            return new Filter { Field = x.Split(":")[0], Value = x.Split(":").Length > 1 ? x.Split(":")[1] : "" };
        }).ToList();
        var sort = sorting.Sort.Select(x => new Sort() { Name = x.Split(":")[0], Direction = x.Split(":").Length > 1 ? x.Split(":")[1] : "asc" }).ToList();

        if (pagination is null)
        {

        }
        try
        {
            var connection = service.DbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            var storedProcedure = new FindCatalogStoredProcedure(catalog, filtering.Ids, filtering.Search, filtering.Fields, filter, sort, page, pageSize, typeView, searchFields: searchFields);

            var multipleResult = await connection.QueryMultipleAsync(storedProcedure.StoredProcedureName, storedProcedure.Parameters);
            totalItems = await multipleResult.ReadSingleAsync<long>();
            data = await multipleResult.ReadAsync<object>();

        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(Result.Failure(new Error("400", ex.Message)));
        }
        var result = new PagedResult<object>()
        {
            PageNumber = page,
            PageSize = pageSize,
            Items = data.ToList(),
            TotalRecode = totalItems
        };
        return TypedResults.Ok(Result.Success(result));
    }
    private async Task<IResult> GetViewAsync([AsParameters] CatalogService service, string typeSize)
    {
        var cs = service.DbContext.Database.GetDbConnection().ConnectionString;
        string sql = $@"SELECT *
                         FROM dbo.DataColorSize where DataType= '{typeSize}'";

        await using var con = new SqlConnection(cs);
        var rows = (await con.QueryAsync<DataColorSize>(sql)).ToList();

        var result = new ApiResponse<List<DataColorSize>>
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
    private async Task<Results<Ok<ApiResponse<object>>, BadRequest<ApiResponse<string>>>> FindAll(
        [AsParameters] CatalogService service,
        [AsParameters] FilteringRequest filtering,
        [AsParameters] SortRequest sorting,
        [AsParameters] PaginationRequest pagination,
        string catalog, string? typeView, string? searchFields)
    {
        long totalItems;
        IEnumerable<object> data;
        var page = pagination?.Page > 0 ? (int)pagination.Page : 1;
        //var pageSize = pagination?.PageSize > 1 ? (pagination.PageSize > 250 ? 250 : (int)pagination.PageSize) : 50;  
        var pageSize = pagination?.PageSize ?? 1000;
        var filter = filtering.Filter.Select(x =>
        {
            if (x.Split(":").Length == 3) return new Filter { Field = x.Split(":")[0], Operator = x.Split(":")[1], Value = x.Split(":")[2] };
            else if (x.Split(":").Length > 3) return new Filter { Field = x.Split(":")[0], Operator = x.Split(":")[1], Value = x.Split(":", 3)[2] };
            return new Filter { Field = x.Split(":")[0], Value = x.Split(":").Length > 1 ? x.Split(":")[1] : "" };
        }).ToList();
        var sort = sorting.Sort.Select(x => new Sort() { Name = x.Split(":")[0], Direction = x.Split(":").Length > 1 ? x.Split(":")[1] : "asc" }).ToList();

        if (pagination is null)
        {

        }
        try
        {
            var connection = service.DbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            var storedProcedure = new FindCatalogStoredProcedure(catalog, filtering.Ids, filtering.Search, filtering.Fields, filter, sort, page, pageSize, typeView, searchFields: searchFields);

            var multipleResult = await connection.QueryMultipleAsync(storedProcedure.StoredProcedureName, storedProcedure.Parameters);
            totalItems = await multipleResult.ReadSingleAsync<long>();
            data = await multipleResult.ReadAsync<object>();
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ApiResponseFactory<string>.BadRequest(ex.Message));
        }

        return TypedResults.Ok(ApiResponseFactory<object>.Ok(data, $"/api/v1/catalog/{catalog}" + filtering.ToQueryString(), totalItems, page, pageSize, sort));
    }

    private async Task<Results<Ok<ApiResponse<string>>, BadRequest<ApiResponse<string>>>> Create([FromServices] CatalogDbContext dbContext, string catalog, IDictionary<string, object> itemToCreate)
    {
        try
        {
            var connection = dbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }
            //DapperPlusManager.Entity<ExpandoObject>("Expando_Object").Table(catalog);

            //itemToCreate.Remove("Id");
            //itemToCreate["CodeUnit"] = 888;

            //await connection.BulkInsertAsync("Expando_Object", new List<ExpandoObject> { itemToCreate.ToExpando() });

            return TypedResults.Ok(ApiResponseFactory<string>.Created());
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ApiResponseFactory<string>.BadRequest(ex.Message));
        }
    }

    private async Task<Results<Ok<ApiResponse<string>>, BadRequest<ApiResponse<string>>>> Update(
        [FromServices] CatalogDbContext dbContext, string catalog, IDictionary<string, object> itemToUpdate)
    {
        try
        {
            var connection = dbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            //DapperPlusManager.Entity<ExpandoObject>("Expando_Object").Table(catalog);
            //await connection.SingleUpdateAsync("Expando_Object", itemToUpdate);

            return TypedResults.Ok(ApiResponseFactory<string>.NoContent());
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ApiResponseFactory<string>.BadRequest(ex.Message));
        }
    }

    private async Task<Results<Ok<ApiResponse<string>>, BadRequest<ApiResponse<string>>>> Delete(
        [FromServices] CatalogDbContext dbContext, string catalog, Guid id)
    {
        try
        {
            var connection = dbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            await connection.ExecuteAsync($"DELETE FROM {catalog} WHERE Id = @Id", new { Id = id });

            return TypedResults.Ok(ApiResponseFactory<string>.NoContent());
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ApiResponseFactory<string>.BadRequest(ex.Message));
        }
    }

    private async Task<bool> DeleteEntity(
        [AsParameters] CatalogService service,
        SmartDeleteDataQuery request)
    {
        ArgumentNullException.ThrowIfNull(request);
        // var dataServices = new SmartDataServices();

        var obj = new
        {
            request.Parameter,
            request.TableName,
            request.KeyData,
            request.DataPlus,
            MaUser = service.CurrentUser.CodeUser,
            service.CurrentUser.CodeUnit,
        };
        var isSuccess = await service.SmartDataServices.ExcuteNonQueryAsync(request.StoreName, service.DbContext.Database.GetConnectionString(), obj);

        if (!isSuccess)
        {
            //Log.Error("User: '{userName}' delete failed data with type: '{entityType}' content: '{json}'.",
            //    currentUser.CodeUser, request.Parameter, JsonConvert.SerializeObject(request));
            return false;
        }

        await service.CacheService.RemoveByPatternAsync(GetCatalogCacheKey(service.CurrentUser.TenantId!, request.TableName));
        //Log.Information("User: '{userName}' deleted data with type: '{entityType}' content: '{json}'.",
        //    currentUser.CodeUser, request.Parameter, JsonConvert.SerializeObject(request));
        return true;

    }
    private string GetCatalogCacheKey(string tenantId, string id)
    {
        return $"KT:{tenantId}:catalog:{id}";
    }
    private string GetMenuCacheKey(string tenantId, string id)
    {
        return $"KT:{tenantId}:menu:*";
    }
}
