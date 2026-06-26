using Carter;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.HRM.Features;

public class HrmCatalogApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Catalog");

        var api = vApi.MapGroup("HRMcatalog/").HasApiVersion(1.0).RequireAuthorization();

        // Routes for querying object.

        api.MapGet("{catalog}", FindAll)
            .WithName("HRMListItems")
            .WithSummary("Danh sách các mục trong danh mục.")
            .WithDescription("Nhận danh sách các mục được phân trang trong danh mục.")
            .WithTags("HRMcatalog");

        api.MapGet("find-cache/{catalog}", FindAllCache)
            .WithName("HRMListItemsCache")
            .WithSummary("Danh sách các mục trong danh mục.")
            .WithDescription("Nhận danh sách các mục được phân trang trong danh mục.")
            .WithTags("HRMcatalog");

        api.MapPost("get-catalog", GetDynamicEnitity)
            .WithName("HRMget-catalog")
            .WithSummary("Danh sách các mục trong danh mục.")
            .WithDescription("Nhận danh sách các mục được phân trang trong danh mục.")
            .WithTags("HRMcatalog");

        // Routes for modifying object.

        api.MapPost("{catalog}", Create)
            .WithName("HRMCreateItem")
            .WithSummary("Tạo một mục trong danh mục.")
            .WithDescription("Tạo một mục mới trong danh mục.")
            .WithTags("HRMcatalog");

        api.MapPut("{catalog}", Update)
            .WithName("HRMUpdateItem")
            .WithSummary("Tạo hoặc thay thế một mục trong danh mục.")
            .WithDescription("Tạo hoặc thay thế một mục trong danh mục.")
            .WithTags("HRMcatalog");

        api.MapDelete("{catalog}/{id:guid}", Delete)
            .WithName("HRMDeleteItem")
            .WithSummary("Xóa mục trong danh mục.")
            .WithDescription("Xóa một mục chỉ định trong danh mục.")
            .WithTags("HRMcatalog");


        api.MapPost("dynamic-create-object", CreateEntityFromJson)
            .WithName("HRMdynamic-create-object")
            .WithSummary("Thêm động dữ liệu danh mục")
            .WithTags("HRMcatalog");

        api.MapPost("HRMdynamic-add-list-data", DynamicAddListData)
            .WithName("HRMdynamic-add-list-data")
            .WithSummary("Thêm động dữ liệu list danh mục")
            .WithTags("HRMcatalog");

        api.MapPut("dynamic-update-object", UpdateDynamicObject)
            .WithName("HRMdynamic-update-object")
            .WithSummary("Cập nhật động dữ liệu  danh mục")
            .WithTags("HRMcatalog");


        api.MapPut("delete-catalog", DeleteEntity)
            .WithName("HRMdelete-catalog")
            .WithSummary("Xoá danh mục động theo tên bảng và khoá chính")
            .WithTags("HRMcatalog");

        api.MapPost("check-exits", CheckDynamicEnitity)
            .WithName("HRMcheck-exits")
            .WithSummary("Check xem model có tồn tại không.")
            .WithDescription("Check xem model có tồn tại trong hệ thống qua các tham số truyền vào không.")
            .WithTags("HRMcatalog");

        api.MapPost("dynamic-add-bulk-data", DynamicAddBulkData)
                .WithName("dynamic-add-bulk-data")
                .WithSummary("Thêm danh sách")
                .WithTags("HRMcatalog");
        api.MapPost("dynamic-add-many-data", DynamicAddManyEntity)
        .WithName("dynamic-add-many-data")
        .WithSummary("Thêm nhiều Enity cùng lúc")
        .WithTags("HRMcatalog");

        //api.MapPost("sync-config", SyncConfig)
        //   .WithName("HRMSyncConfig")
        //   .WithSummary("Đồng bộ cấu hình thông báo")
        //   .WithDescription("Gọi store sp_HRMSyncConfig để đồng bộ cấu hình")
        //   .WithTags("HRMcatalog");
    }

    private async Task<IResult> FindAllCache(
        [AsParameters] HrmCatalogService service,
        [AsParameters] FilteringRequest filtering,
        [AsParameters] SortRequest sorting,
        [AsParameters] PaginationRequest pagination,
        string catalog, string? typeView)
    {
        var query = new GetDynamicCatalogQuery(filtering, sorting, pagination, catalog, typeView);
        var result = await service.Mediator.Send(query);
        return Results.Ok(result);
    }

    //private async Task<IResult> FindAllCache( [AsParameters] HrmCatalogService service,
    //    [AsParameters] FilteringRequest filtering,
    //    [AsParameters] SortRequest sorting,
    //    [AsParameters] PaginationRequest pagination,
    //    string catalog, string? typeView,
    //    CancellationToken token)
    //{
    //    var query = new GetDynamicCatalogQuery(filtering, sorting, pagination, catalog, typeView);
    //    var result = await service.Mediator.Send(query, token);
    //    return Results.Ok(result);
    //}

    private async Task<IResult> DynamicAddListData(
        [AsParameters] HrmCatalogService service,
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
        [AsParameters] HrmCatalogService service,
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

        service.Logger.LogInformation("User: '{userName}' Edited data with type: '{entityType}' content: '{json}'.",
            service.CurrentUser.CodeUser, request.EntityType, request.JsonData);
        return Results.Ok("Entity updated successfully.");
    }

    private async Task<IResult> CreateEntityFromJson(
        [AsParameters] HrmCatalogService service,
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

        //Log.Information("User: '{userName}' created data with type: '{entityType}' content: '{json}'.", currentUser.CodeUser, entityType, jsonData);

        return Results.Ok("Entity created successfully.");
    }

    //[AllowAnonymous]
    public async Task<IEnumerable<object>> GetDynamicEnitity(
        [AsParameters] HrmCatalogService service,
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
      [AsParameters] HrmCatalogService service,
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

        var assemblyHRM = Assembly.Load("Catalog.HRM");
        var typeHrm = assemblyHRM.GetTypes().FirstOrDefault(t => t.Name == entityType);
        if (typeHrm is not null) return typeHrm;


        throw new InvalidOperationException($"Type '{entityType}' không tồn tại trong assembly 'Catalog.HRM'.");

    }

    private async Task<Results<Ok<ApiResponse<object>>, BadRequest<ApiResponse<string>>>> FindAll(
        [AsParameters] HrmCatalogService service,
        [AsParameters] FilteringRequest filtering,
        [AsParameters] SortRequest sorting,
        [AsParameters] PaginationRequest pagination,
        string catalog, string? typeView)
    {
        long totalItems;
        IEnumerable<object> data;
        var page = pagination?.Page > 0 ? (int)pagination.Page : 1;
        //var pageSize = pagination?.PageSize > 1 ? (pagination.PageSize > 250 ? 250 : (int)pagination.PageSize) : 50;  
        var pageSize = pagination?.PageSize ?? 1000;
        var filter = filtering.Filter.Select(x =>
        {
            if (x.Split(":").Length == 3) return new Filter { Field = x.Split(":")[0], Operator = x.Split(":")[1], Value = x.Split(":")[2] };

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

            var storedProcedure = new FindCatalogStoredProcedure(catalog, filtering.Ids, filtering.Search, filtering.Fields, filter, sort, page, pageSize, typeView);

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

    private async Task<Results<Ok<ApiResponse<string>>, BadRequest<ApiResponse<string>>>> Create([FromServices] HrmDbContext dbContext, string catalog, IDictionary<string, object> itemToCreate)
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
        [FromServices] HrmDbContext dbContext, string catalog, IDictionary<string, object> itemToUpdate)
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
        [FromServices] HrmDbContext dbContext, string catalog, Guid id)
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

    //public async Task<bool> SyncConfig(
    //[AsParameters] HrmCatalogService service,
    //SyncConfigRequest request)
    //{
    //    ArgumentNullException.ThrowIfNull(request);

    //    var obj = new
    //    {
    //        request.Param,
    //        request.CodeUnit,
    //        request.CodeUser
    //    };

    //    var isSuccess = await service.SmartDataServices.ExcuteNonQueryAsync(
    //        "sp_HRMSyncConfig",
    //        service.DbContext.Database.GetConnectionString(),
    //        obj
    //    );

    //    return isSuccess;
    //}


    private async Task<bool> DeleteEntity(
        [AsParameters] HrmCatalogService service,
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

    private async Task<IResult> DynamicAddBulkData(
    [AsParameters] HrmCatalogService service,
    DynamicListDataBulkModel request)
    {
        var type = GetEntityType(request.EntityType);
        var listType = typeof(List<>).MakeGenericType(type);

        if (!string.IsNullOrWhiteSpace(request.JsonDataCreate))
        {
            if (JsonConvert.DeserializeObject(request.JsonDataCreate, listType) is IEnumerable<object> entityCreate)
            {
                await service.DbContext.AddRangeEntities(entityCreate, CancellationToken.None);
            }
        }

        var rsl = await service.DbContext.SaveChangesAsync() > 0;

        await service.CacheService
            .RemoveByPatternAsync(GetCatalogCacheKey(service.CurrentUser.TenantId!, request.EntityType));

        return Results.Ok(new
        {
            Success = rsl,
            Message = $"Bulk insert {request.EntityType} successfully."
        });
    }

    private async Task<IResult> DynamicAddManyEntity(
        [AsParameters] HrmCatalogService service,
        BulkMultiEntityRequest request)
    {
        if (request == null || request.Items.Count == 0)
            return Results.BadRequest(new { Error = "No data provided." });

        var results = new List<object>();

        using var tran = await service.DbContext.Database.BeginTransactionAsync();
        try
        {
            foreach (var item in request.Items)
            {
                if (string.IsNullOrWhiteSpace(item.EntityType))
                {
                    results.Add(new { Entity = "UNKNOWN", Error = "Missing EntityType" });
                    continue;
                }

                var entityType = GetEntityType(item.EntityType);
                if (entityType == null)
                {
                    results.Add(new { Entity = item.EntityType, Error = "Entity type not found" });
                    continue;
                }

                var listType = typeof(List<>).MakeGenericType(entityType);
                IEnumerable<object>? entityList;
                try
                {
                    entityList = JsonConvert.DeserializeObject(item.JsonDataCreate, listType) as IEnumerable<object>;
                }
                catch (JsonException ex)
                {
                    results.Add(new { Entity = item.EntityType, Error = "Invalid JSON", Detail = ex.Message });
                    continue;
                }

                if (entityList == null || !entityList.Any())
                {
                    results.Add(new { Entity = item.EntityType, Error = "Empty list" });
                    continue;
                }

                // === THÊM ĐOẠN FIX DATETIME OUT-OF-RANGE ===
                foreach (var entity in entityList)
                {
                    var properties = entity.GetType().GetProperties()
                        .Where(p => p.PropertyType == typeof(DateTime) && p.CanRead && p.CanWrite);

                    foreach (var prop in properties)
                    {
                        var value = (DateTime)prop.GetValue(entity)!;
                        if (value == default(DateTime) || value.Year < 1753)
                        {
                            // Gán giá trị an toàn: ngày hiện tại (có thể thay bằng new DateTime(1900,1,1) nếu muốn)
                            prop.SetValue(entity, DateTime.Now);

                            // Logging để debug (có thể comment lại sau)
                            Console.WriteLine($"[FIX DATETIME] Entity: {entity.GetType().Name}, Property: {prop.Name} " +
                                              $"was {value:yyyy-MM-dd HH:mm:ss} → fixed to {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                        }
                    }
                }
                // ===========================================

                await service.DbContext.AddRangeAsync(entityList);

                results.Add(new
                {
                    Entity = item.EntityType,
                    Inserted = entityList.Count()
                });
            }

            // Optional: Thêm kiểm tra toàn bộ entries trước khi Save (có thể bỏ sau khi ổn định)
            // var suspiciousEntries = service.DbContext.ChangeTracker.Entries()
            //     .Where(e => e.State == EntityState.Added)
            //     .SelectMany(e => e.Properties)
            //     .Where(p => p.Metadata.ClrType == typeof(DateTime) && 
            //                 !p.Metadata.IsNullable && 
            //                 (DateTime?)p.CurrentValue < new DateTime(1753,1,1));
            // if (suspiciousEntries.Any())
            // {
            //     foreach (var p in suspiciousEntries)
            //     {
            //         Console.WriteLine($"[WARNING] Still invalid date: {p.EntityEntry.Entity.GetType().Name}.{p.Metadata.Name} = {p.CurrentValue}");
            //     }
            // }

            await service.DbContext.SaveChangesAsync();
            await tran.CommitAsync();

            // Clear cache theo từng entity type
            foreach (var item in request.Items)
            {
                if (!string.IsNullOrWhiteSpace(item.EntityType))
                {
                    await service.CacheService.RemoveByPatternAsync(
                        GetCatalogCacheKey(service.CurrentUser.TenantId!, item.EntityType)
                    );
                }
            }

            return Results.Ok(new
            {
                Success = true,
                Results = results
            });
        }
        catch (Exception ex)
        {
            await tran.RollbackAsync();

            // Log inner exception để dễ debug hơn
            var innerMessage = ex.InnerException?.Message ?? ex.Message;
            return Results.Problem($"Internal Server Error: {innerMessage}");
        }
    }



}
