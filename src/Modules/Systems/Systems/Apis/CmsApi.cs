using BuildingBlocks.Pagination.Version1;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using Systems.Features.Cms;
using Systems.Infrastructure.Entities;

namespace Systems.Apis;

public static class CmsApi
{
    public static RouteGroupBuilder MapCmsApiV1(this IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Cms");
        var api = vApi.MapGroup("cms").HasApiVersion(1.0);

        api.MapGet("/tables/{tableId}/column-names", FindColumnNames)
            .WithName("FindColumnNames")
            .WithSummary("Danh sách các cột trong bảng dưới dạng một chuỗi cách nhau bới dấu \",\".")
            .WithDescription("Lấy danh sách các cột trong bảng dưới dạng một chuỗi cách nhau bới dấu \",\".")
            .WithTags("ColumnNames");

        api.MapGet("/settings", FindComponentSetting)
            .WithName("FindComponentSetting")
            .WithSummary("Danh sách cấu hình.")
            .WithDescription("Lấy danh sách các mục trong cấu hình.")
            .WithTags("ComponentSettings");

        api.MapGet("/settings/{settingId}", FindOneComponentSetting)
            .WithName("FindOneComponentSetting")
            .WithSummary("Lấy chi tiết cấu hình theo id.")
            .WithDescription("Lấy chi tiết cấu hình theo id.")
            .WithTags("ComponentSettings");

        api.MapPost("/settings", CreateComponentSetting)
            .WithName("CreateComponentSetting")
            .WithSummary("Tạo mới cấu hình.")
            .WithDescription("Tạo một mục mới trong cấu hình.")
            .WithTags("ComponentSettings");

        api.MapPut("/settings/{settingId:guid}", UpdateComponentSetting)
            .WithName("UpdateComponentSetting")
            .WithSummary("Cập nhật cấu hình.")
            .WithDescription("Cập nhật một mục trong cấu hình.")
            .WithTags("ComponentSettings");

        api.MapDelete("/settings/{settingId:guid}", DeleteComponentSetting)
            .WithName("DeleteComponentSetting")
            .WithSummary("Xóa cấu hình.")
            .WithDescription("Xóa một mục trong cấu hình.")
            .WithTags("ComponentSettings");

        api.MapGet("/settings/{settingId:guid}/properties", FindComponentProperties)
            .WithName("FindComponentProperties")
            .WithSummary("Danh sách thuộc tính thành phần của cấu hình.")
            .WithDescription("Lấy danh sách thuộc tính của cấu hình.")
            .WithTags("ComponentSettings");

        api.MapGet("/settings/get-component-properties/{keyCms}", FindComponentPropertiesByKey)
            .WithName("FindComponentPropertiesWithKey")
            .WithSummary("Danh sách thuộc tính thành phần của cấu hình.")
            .WithDescription("Lấy danh sách thuộc tính của cấu hình theo key.")
            .WithTags("ComponentSettings");

        api.MapPost("/settings/{settingId:guid}/generate-properties", GenerateComponentProperties)
            .WithName("GenerateComponentProperties")
            .WithSummary("Tạo thuộc tính cấu hình.")
            .WithDescription("Tạo thuộc tính của một cấu hình.")
            .WithTags("ComponentSettings");

        api.MapPut("/settings/{settingId:guid}/properties", UpdateComponentProperties)
            .WithName("UpdateComponentProperties")
            .WithSummary("Cập nhật danh sách thuộc tính.")
            .WithDescription("Cập nhật danh sách thuộc tính theo cấu hình.")
            .WithTags("ComponentSettings");


        api.MapGet("/properties", FindComponentProperty)
            .WithName("FindComponentProperty")
            .WithSummary("Danh sách cấu hình.")
            .WithDescription("Lấy danh sách các mục trong cấu hình.")
            .WithTags("ComponentProperties");

        api.MapGet("/properties/{propertyId:guid}", FindOneComponentProperty)
            .WithName("FindOneComponentProperty")
            .WithSummary("Lấy chi tiết cấu hình theo id.")
            .WithDescription("Lấy chi tiết cấu hình theo id.")
            .WithTags("ComponentProperties");

        api.MapPost("/properties", CreateComponentProperty)
            .WithName("CreateComponentProperty")
            .WithSummary("Tạo mới thuộc tính.")
            .WithDescription("Tạo mới một mục trong thuộc tính.")
            .WithTags("ComponentProperties");

        api.MapPut("/properties/{propertyId:guid}", UpdateComponentProperty)
            .WithName("UpdateComponentProperty")
            .WithSummary("Cập nhật thuộc tính.")
            .WithDescription("Cập nhật một mục trong thuộc tính.")
            .WithTags("ComponentProperties");
        api.MapPut("/properties/update", UpdateComponentPropertiesByKey)
            .WithName("UpdateComponentPropertiesByKey")
            .WithSummary("Cập nhật thuộc tính bằng mã key.")
            .WithDescription("Cập nhật một mục trong thuộc tính theo key.")
            .WithTags("ComponentProperties");

        api.MapDelete("/properties/{propertyId:guid}", DeleteComponentProperty)
            .WithName("DeleteComponentProperty")
            .WithSummary("Xóa thuộc tính.")
            .WithDescription("Xóa một mục trong thuộc tính.")
            .WithTags("ComponentProperties");

        return api;
    }

    private static async Task<Ok<ApiResponse<string>>> FindColumnNames(
        [AsParameters] SystemService services,
        //[FromHeader(Name = TenantContains.TenantIdHeader)][Required] string tenantId,
        //[FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
        [FromRoute] string tableId,
        CancellationToken token)
    {
        var query = new FindColumnNamesQuery(tableId);
        var result = await services.Mediator.Send(query, token);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> FindComponentSetting(
        [AsParameters] SystemService services,
        [AsParameters] FilteringRequest filtering,
        CancellationToken token)
    {
        var query = new FindComponentSettingQuery(filtering, services.CurrentUser);
        var result = await services.Mediator.Send(query, token);

        return Results.Ok(result);
    }

    private static async Task<Ok<ApiResponse<ComponentSetting>>> FindOneComponentSetting(
        [AsParameters] SystemService services,
        string settingId,
        CancellationToken token)
    {
        var query = new FindOneComponentSettingQuery(settingId);
        var result = await services.Mediator.Send(query, token);

        return TypedResults.Ok(result);
    }

    private static async Task<Created<ApiResponse<ComponentSetting>>> CreateComponentSetting(
        [AsParameters] SystemService services,
         ComponentSetting request)
    {
        var componentSetting = await services.DbContext.ComponentSettings
            .SingleOrDefaultAsync(x => x.Id == request.Id);

        if (componentSetting != null)
        {
            throw new Exception();
        }

        request.Id = Guid.NewGuid();
        request.Created = DateTime.Now;

        services.DbContext.ComponentSettings.Add(request);

        await services.DbContext.SaveChangesAsync();

        var response = await services.DbContext.ComponentSettings.FirstOrDefaultAsync(x => x.Id == request.Id);

        return TypedResults.Created($"/api/v1/cms/settings/{request.Id}", ApiResponseFactory<ComponentSetting>.Created(response));
    }

    private static async Task<Ok<ApiResponse<ComponentSetting>>> UpdateComponentSetting(
        [AsParameters] SystemService services,
        // [FromHeader(Name = TenantContains.TenantIdHeader)][Required] string tenantId,
        //[FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
        Guid settingId,
         ComponentSetting updateItem)
    {

        var componentSetting = await services.DbContext.ComponentSettings
            .Include(x => x.ComponentProperties.OrderBy(y => y.Number))
            .SingleOrDefaultAsync(i => i.Id == settingId);

        if (componentSetting == null)
        {
            throw new Exception($"Không tìm thấy cấu hình với Id : {settingId}.");
        }
        else
        {
            componentSetting.Id = updateItem.Id;
            componentSetting.ComponentCode = updateItem.ComponentCode;
            componentSetting.ComponentName = updateItem.ComponentName;
            componentSetting.TableName = updateItem.TableName;
            componentSetting.RequiredField = updateItem.RequiredField;
            componentSetting.Height = updateItem.Height;
            componentSetting.Width = updateItem.Width;
            componentSetting.HelpUrl = updateItem.HelpUrl;
            componentSetting.HelpUrlHtml = updateItem.HelpUrlHtml;
            componentSetting.PageSize = updateItem.PageSize;
            componentSetting.PageSizes = updateItem.PageSizes;
            componentSetting.ClassGrid = updateItem.ClassGrid;
            componentSetting.AllowGroup = updateItem.AllowGroup;
            componentSetting.EnablePersistence = updateItem.EnablePersistence;
            componentSetting.EnableSum = updateItem.EnableSum;
            componentSetting.AllowPaging = updateItem.AllowPaging;

            var requiredFields = updateItem.RequiredField.Split(",").ToList();

            foreach (var componentProperty in componentSetting.ComponentProperties)
            {
                if (requiredFields.Contains(componentProperty.FieldCode))
                {
                    var updateChildItem = updateItem.ComponentProperties.FirstOrDefault(x => x.FieldCode == componentProperty.FieldCode);
                    if (updateChildItem != null)
                    {
                        componentProperty.FieldCode = updateChildItem.FieldCode;
                        componentProperty.FieldName = updateChildItem.FieldName;
                        componentProperty.Visible = updateChildItem.Visible;
                        componentProperty.Freeze = updateChildItem.Freeze;
                        componentProperty.Width = updateChildItem.Width;
                        componentProperty.MaxWidth = updateChildItem.MaxWidth;
                        componentProperty.MinWidth = updateChildItem.MinWidth;
                        componentProperty.DataType = updateChildItem.DataType;
                        componentProperty.Editor = updateChildItem.Editor;
                        componentProperty.TemplateId = updateChildItem.TemplateId;
                        componentProperty.Placeholder = updateChildItem.Placeholder;
                        componentProperty.Format = updateChildItem.Format;
                        componentProperty.TextAlign = updateChildItem.TextAlign;
                        componentProperty.TextColor = updateChildItem.TextColor;
                        componentProperty.FontStyle = updateChildItem.FontStyle;
                        componentProperty.FontWeight = updateChildItem.FontWeight;
                        componentProperty.BackColor = updateChildItem.BackColor;
                        componentProperty.CssClass = updateChildItem.CssClass;
                        componentProperty.AllowAdding = updateChildItem.AllowAdding;
                        componentProperty.AllowEditing = updateChildItem.AllowEditing;
                        componentProperty.AllowSearching = updateChildItem.AllowSearching;
                        componentProperty.SearchOperator = updateChildItem.SearchOperator;
                        componentProperty.IsGrouping = updateChildItem.IsGrouping;
                        componentProperty.GroupOrder = updateChildItem.GroupOrder;
                        componentProperty.IsSorting = updateChildItem.IsSorting;
                        componentProperty.SortDirection = updateChildItem.SortDirection;
                        componentProperty.SortOrder = updateChildItem.SortOrder;
                        componentProperty.IsAggregate = updateChildItem.IsAggregate;
                        componentProperty.AggregateType = updateChildItem.AggregateType;
                        componentProperty.Expression = updateChildItem.Expression;
                        componentProperty.IsParent = updateChildItem.IsParent;
                        componentProperty.ParentId = updateChildItem.ParentId;
                        componentProperty.IsPrimaryKey = updateChildItem.IsPrimaryKey;
                        componentProperty.ShowInColumnChooser = updateChildItem.ShowInColumnChooser;
                        componentProperty.IsGroupColumn = updateChildItem.IsGroupColumn;
                        componentProperty.GroupColumnKey = updateChildItem.GroupColumnKey;
                        componentProperty.GroupColumnName = updateChildItem.GroupColumnName;

                    }

                    componentProperty.Number = requiredFields.IndexOf(componentProperty.FieldCode) + 1;
                }
                else
                {
                    services.DbContext.ComponentProperties.Remove(componentProperty);
                }
            }
        }

        await services.CacheService.RemoveByPatternAsync($"KT:{services.CurrentUser.TenantId}:setting:*");
        await services.DbContext.SaveChangesAsync();

        return TypedResults.Ok(ApiResponseFactory<ComponentSetting>.Ok(componentSetting));
    }

    private static async Task<NoContent> DeleteComponentSetting(
        [AsParameters] SystemService services,
        //[FromHeader(Name = TenantContains.TenantIdHeader)][Required] string tenantId,
        // [FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
        [FromRoute] Guid settingId)
    {

        var componentSetting = await services.DbContext.ComponentSettings
            .Include(x => x.ContextMenuProperties.OrderBy(y => y.Number))
            .SingleOrDefaultAsync(i => i.Id == settingId);

        if (componentSetting == null)
        {
            throw new Exception($"Không tìm thấy cấu hình với Id : {settingId}.");
        }
        else
        {
            services.DbContext.ComponentSettings.Remove(componentSetting);
        }

        await services.DbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<Ok<ApiResponse<List<ComponentProperty>>>> FindComponentProperties(
        [AsParameters] SystemService services,
        // [FromHeader(Name = TenantContains.TenantIdHeader)][Required] string tenantId,
        // [FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
        [FromRoute] Guid settingId, CancellationToken token)
    {
        var query = new FindComponentPropertiesQuery(settingId);
        var result = await services.Mediator.Send(query, token);
        return TypedResults.Ok(result);
    }
    private static async Task<Ok<ApiResponse<List<ComponentProperty>>>> FindComponentPropertiesByKey(
        [AsParameters] SystemService services,
        [FromRoute] string keyCms, CancellationToken token)
    {
        var query = new FindComponentPropertiesByKeyQuery(keyCms);
        var result = await services.Mediator.Send(query, token);
        return TypedResults.Ok(result);
    }

    private static async Task<Ok<Result>> UpdateComponentPropertiesByKey(
        [AsParameters] SystemService services,
        ComponentPropertyUpdate query, CancellationToken token)
    {
        var queryNew = new UpdateComponentPropertiesQuery(query);
        var result = await services.Mediator.Send(queryNew, token);
        return TypedResults.Ok(result);
    }

    private static async Task<Ok<ApiResponse<ComponentSetting>>> GenerateComponentProperties(
        [AsParameters] SystemService services,
        // [FromHeader(Name = TenantContains.TenantIdHeader)][Required] string tenantId,
        // [FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
        [FromRoute] Guid settingId)
    {
        var componentSetting = await services.DbContext.ComponentSettings
            .FirstAsync(x => x.Id == settingId);

        if (componentSetting == null)
        {
            throw new Exception($"Cấu hình {settingId} không tồn tại!");
        }

        var connection = services.DbContext.Database.GetDbConnection();
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        await connection.ExecuteAsync($"EXEC [dbo].[GenerateProperties] @Id", new { Id = componentSetting.ComponentCode });

        var componentSettingUpdated = await services.DbContext.ComponentSettings
            .Include(x => x.ComponentProperties.OrderBy(x => x.Number))
            .Include(x => x.ContextMenuProperties.OrderBy(x => x.Number))
            .FirstOrDefaultAsync(x => x.Id == settingId);

        var response = ApiResponseFactory<ComponentSetting>.Ok(componentSetting);

        return TypedResults.Ok(response);
    }

    private static async Task<Ok<ApiResponse<List<ComponentProperty>>>> FindComponentProperty(
        [AsParameters] SystemService services,
        // [FromHeader(Name = TenantContains.TenantIdHeader)][Required] string tenantId,
        // [FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
        [AsParameters] FilteringRequest filtering)
    {
        var componentProperties = await services.DbContext.ComponentProperties
            .Where(x => filtering.Ids.Contains(x.Id.ToString()))
            .ToListAsync();

        var response = ApiResponseFactory<List<ComponentProperty>>.Ok(componentProperties);

        return TypedResults.Ok(response);
    }

    private static async Task<Ok<ApiResponse<ComponentProperty>>> FindOneComponentProperty(
        [AsParameters] SystemService services,
        //[FromHeader(Name = TenantContains.TenantIdHeader)][Required] string tenantId,
        // [FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
        [FromRoute] Guid propertyId)
    {
        var componentProperty = await services.DbContext.ComponentProperties
            .SingleOrDefaultAsync(x => x.Id == propertyId);

        if (componentProperty == null)
        {
            throw new Exception($"Thuộc tính {propertyId} không tồn tại!");
        }

        var response = ApiResponseFactory<ComponentProperty>.Ok(componentProperty);

        return TypedResults.Ok(response);
    }

    private static async Task<Ok<ApiResponse<string>>> UpdateComponentProperties(
            [AsParameters] SystemService services,
            // [FromHeader(Name = TenantContains.TenantIdHeader)][Required] string tenantId,
            // [FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
            [FromRoute] Guid settingId,
            [FromBody] List<ComponentProperty> request)
    {

        var componentSetting = await services.DbContext.ComponentSettings
            .Include(x => x.ComponentProperties)
            .SingleOrDefaultAsync(i => i.Id == settingId);

        if (componentSetting == null)
        {
            throw new Exception($"Không tìm thấy cấu hình với Id : {settingId}.");
        }
        else
        {
            foreach (var updateItem in request)
            {
                var componentProperty = componentSetting.ComponentProperties.FirstOrDefault(x => x.Id == updateItem.Id);
                if (componentProperty != null)
                {
                    componentProperty.FieldCode = updateItem.FieldCode;
                    componentProperty.FieldName = updateItem.FieldName;
                    componentProperty.Visible = updateItem.Visible;
                    componentProperty.Width = updateItem.Width;
                    componentProperty.MaxWidth = updateItem.MaxWidth;
                    componentProperty.MinWidth = updateItem.MinWidth;
                    componentProperty.DataType = updateItem.DataType;
                    componentProperty.Editor = updateItem.Editor;
                    componentProperty.TemplateId = updateItem.TemplateId;
                    componentProperty.Placeholder = updateItem.Placeholder;
                    componentProperty.Format = updateItem.Format;
                    componentProperty.TextAlign = updateItem.TextAlign;
                    componentProperty.TextColor = updateItem.TextColor;
                    componentProperty.FontStyle = updateItem.FontStyle;
                    componentProperty.FontWeight = updateItem.FontWeight;
                    componentProperty.BackColor = updateItem.BackColor;
                    componentProperty.CssClass = updateItem.CssClass;
                    componentProperty.AllowAdding = updateItem.AllowAdding;
                    componentProperty.AllowEditing = updateItem.AllowEditing;
                    componentProperty.AllowSearching = updateItem.AllowSearching;
                    componentProperty.SearchOperator = updateItem.SearchOperator;
                    componentProperty.IsGrouping = updateItem.IsGrouping;
                    componentProperty.GroupOrder = updateItem.GroupOrder;
                    componentProperty.IsSorting = updateItem.IsSorting;
                    componentProperty.SortDirection = updateItem.SortDirection;
                    componentProperty.SortOrder = updateItem.SortOrder;
                    componentProperty.IsAggregate = updateItem.IsAggregate;
                    componentProperty.AggregateType = updateItem.AggregateType;
                    componentProperty.Expression = updateItem.Expression;
                    componentProperty.IsParent = updateItem.IsParent;
                    componentProperty.ParentId = updateItem.ParentId;
                    componentProperty.IsPrimaryKey = updateItem.IsPrimaryKey;
                    componentProperty.ShowInColumnChooser = updateItem.ShowInColumnChooser;
                    componentProperty.IsGroupColumn = updateItem.IsGroupColumn;
                    componentProperty.GroupColumnKey = updateItem.GroupColumnKey;
                    componentProperty.GroupColumnName = updateItem.GroupColumnName;
                }
            }
        }

        await services.DbContext.SaveChangesAsync();

        return TypedResults.Ok(ApiResponseFactory<string>.Ok("Cập nhật thuộc tính thành công!"));
    }

    private static async Task<Created<ApiResponse<ComponentProperty>>> CreateComponentProperty(
        [AsParameters] SystemService services,
        // [FromHeader(Name = TenantContains.TenantIdHeader)][Required] string tenantId,
        // [FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
        [FromBody] ComponentProperty request)
    {

        var componentProperty = await services.DbContext.ComponentProperties
            .SingleOrDefaultAsync(x => x.Id == request.Id);

        if (componentProperty != null)
        {
            throw new Exception();
        }

        services.DbContext.ComponentProperties.Add(request);

        await services.DbContext.SaveChangesAsync();

        var response = await services.DbContext.ComponentProperties.FirstOrDefaultAsync(x => x.Id == request.Id);

        return TypedResults.Created($"/api/v1/cms/settings/{request.Id}", ApiResponseFactory<ComponentProperty>.Created(response));
    }

    private static async Task<Ok<ApiResponse<ComponentProperty>>> UpdateComponentProperty(
        [AsParameters] SystemService services,
        // [FromHeader(Name = TenantContains.TenantIdHeader)][Required] string tenantId,
        //  [FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
        [FromRoute] Guid propertyId,
        [FromBody] ComponentProperty insertItem)
    {

        var componentProperty = await services.DbContext.ComponentProperties
            .SingleOrDefaultAsync(i => i.Id == propertyId);

        if (componentProperty == null)
        {
            throw new Exception($"Không tìm thấy thuộc tính với Id : {propertyId}.");
        }
        else
        {
            componentProperty.Id = insertItem.Id;
            componentProperty.FieldCode = insertItem.FieldCode;
            componentProperty.FieldName = insertItem.FieldName;
            componentProperty.Visible = insertItem.Visible;
            componentProperty.Width = insertItem.Width;
            componentProperty.MaxWidth = insertItem.MaxWidth;
            componentProperty.MinWidth = insertItem.MinWidth;
            componentProperty.DataType = insertItem.DataType;
            componentProperty.Editor = insertItem.Editor;
            componentProperty.TemplateId = insertItem.TemplateId;
            componentProperty.Placeholder = insertItem.Placeholder;
            componentProperty.Format = insertItem.Format;
            componentProperty.TextAlign = insertItem.TextAlign;
            componentProperty.TextColor = insertItem.TextColor;
            componentProperty.FontStyle = insertItem.FontStyle;
            componentProperty.FontWeight = insertItem.FontWeight;
            componentProperty.BackColor = insertItem.BackColor;
            componentProperty.CssClass = insertItem.CssClass;
            componentProperty.AllowAdding = insertItem.AllowAdding;
            componentProperty.AllowEditing = insertItem.AllowEditing;
            componentProperty.AllowSearching = insertItem.AllowSearching;
            componentProperty.SearchOperator = insertItem.SearchOperator;
            componentProperty.IsGrouping = insertItem.IsGrouping;
            componentProperty.GroupOrder = insertItem.GroupOrder;
            componentProperty.IsSorting = insertItem.IsSorting;
            componentProperty.SortDirection = insertItem.SortDirection;
            componentProperty.SortOrder = insertItem.SortOrder;
            componentProperty.IsAggregate = insertItem.IsAggregate;
            componentProperty.AggregateType = insertItem.AggregateType;
            componentProperty.Expression = insertItem.Expression;
            componentProperty.IsParent = insertItem.IsParent;
            componentProperty.ParentId = insertItem.ParentId;
            componentProperty.IsPrimaryKey = insertItem.IsPrimaryKey;
            componentProperty.ShowInColumnChooser = insertItem.ShowInColumnChooser;
            componentProperty.IsGroupColumn = insertItem.IsGroupColumn;
            componentProperty.GroupColumnKey = insertItem.GroupColumnKey;
            componentProperty.GroupColumnName = insertItem.GroupColumnName;
        }

        await services.DbContext.SaveChangesAsync();

        return TypedResults.Ok(ApiResponseFactory<ComponentProperty>.Ok(componentProperty));
    }

    private static async Task<NoContent> DeleteComponentProperty(
        [AsParameters] SystemService services,
        // [FromHeader(Name = TenantContains.TenantIdHeader)][Required] string tenantId,
        // [FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
        [FromRoute] Guid propertyId)
    {

        var componentProperty = await services.DbContext.ComponentProperties
            .SingleOrDefaultAsync(i => i.Id == propertyId);

        if (componentProperty == null)
        {
            throw new Exception($"Không tìm thấy cấu hình với Id : {propertyId}.");
        }
        else
        {
            services.DbContext.ComponentProperties.Remove(componentProperty);
        }

        await services.DbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}
