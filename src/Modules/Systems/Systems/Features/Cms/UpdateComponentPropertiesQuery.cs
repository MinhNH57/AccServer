using BuildingBlocks.CQRS;
using BuildingBlocks.Web;
using System.Linq;
using Systems.Infrastructure;
using Systems.Infrastructure.Entities;

namespace Systems.Features.Cms;

public class ComponentPropertyUpdate
{
    public string KeyComponent { get; set; } = string.Empty;
    public List<ComponentProperty> ComponentProperties { get; set; } = new();
}

public record UpdateComponentPropertiesQuery(ComponentPropertyUpdate Query) : IQuery<Result>;

public class UpdateComponentPropertiesQueryHandler(SystemDbcontext dbContext, RedisCacheService cacheService, ICurrentUser currentUser) : IQueryHandler<UpdateComponentPropertiesQuery, Result>
{
    public async Task<Result> Handle(UpdateComponentPropertiesQuery query, CancellationToken cancellationToken)
    {
        var componentProUpdates = await dbContext.ComponentProperties.Where(x => x.ComponentCode == query.Query.KeyComponent).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        //var componentProUpdates = await dbContext.ComponentProperties.Where(x => x.ComponentCode == query.Query.KeyComponent 
        //    && x.ShowInColumnChooser).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        if (!componentProUpdates.Any())
        {
            return Result.Failure(new Error("400", "Không tìm thấy danh sách thuộc tính cần cập nhật"));
        }
        foreach (var componentProperty in query.Query.ComponentProperties)
        {
            var updateChildItem = componentProUpdates.FirstOrDefault(x => x.FieldCode == componentProperty.FieldCode);
            if (updateChildItem != null)
            {
                updateChildItem.FieldName = componentProperty.FieldName;
                updateChildItem.Visible = componentProperty.Visible;
                updateChildItem.Freeze = componentProperty.Freeze;
                updateChildItem.Width = componentProperty.Width;
                updateChildItem.Number = componentProperty.Number;
            }
        }
         //Chỗ này dùng để update lại trường cần lấy nhé.
        string requiredFieldUpdate = string.Join(",", componentProUpdates.OrderBy(x=>x.Number).Select(x => x.FieldCode).ToList());
        var componentSetting = await dbContext.ComponentSettings
            .FirstOrDefaultAsync(x => x.ComponentCode == query.Query.KeyComponent, cancellationToken: cancellationToken);

        if (componentSetting is not null)
        {
            componentSetting.RequiredField = requiredFieldUpdate;
            dbContext.ComponentSettings.Update(componentSetting); 
        } 

        dbContext.ComponentProperties.UpdateRange(componentProUpdates);
        var count = await dbContext.SaveChangesAsync(cancellationToken);
        if (count > 0)
        {
            await cacheService.RemoveByPatternAsync($"KT:{currentUser.TenantId}:setting:{query.Query.KeyComponent}");

            return Result.Success(true);
        }
        return Result.Failure(new Error("400", "Có lỗi khi cất giữ"));
    }
}
