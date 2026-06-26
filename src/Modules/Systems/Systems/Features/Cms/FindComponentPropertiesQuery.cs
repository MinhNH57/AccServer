using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using Systems.Infrastructure;
using Systems.Infrastructure.Entities;

namespace Systems.Features.Cms;

public record FindComponentPropertiesQuery(Guid SettingId) : IQuery<ApiResponse<List<ComponentProperty>>>;
public record FindComponentPropertiesByKeyQuery(string KeyCms) : IQuery<ApiResponse<List<ComponentProperty>>>;


public class FindComponentPropertiesQueryHandler(SystemDbcontext dbContext) : IQueryHandler<FindComponentPropertiesQuery, ApiResponse<List<ComponentProperty>>>
{
    public async Task<ApiResponse<List<ComponentProperty>>> Handle(FindComponentPropertiesQuery query, CancellationToken cancellationToken)
    {
        var componentSetting = await dbContext.ComponentSettings
            .Include(x => x.ComponentProperties)
            .FirstAsync(x => x.Id == query.SettingId, cancellationToken: cancellationToken);

        return ApiResponseFactory<List<ComponentProperty>>.Ok(componentSetting.ComponentProperties.ToList());

    }
}
public class FindComponentPropertiesByKeyHandler(SystemDbcontext dbContext) : IQueryHandler<FindComponentPropertiesByKeyQuery, ApiResponse<List<ComponentProperty>>>
{
    public async Task<ApiResponse<List<ComponentProperty>>> Handle(FindComponentPropertiesByKeyQuery query, CancellationToken cancellationToken)
    {
        var componentSetting = await dbContext.ComponentProperties.Where(x => x.ComponentCode == query.KeyCms && x.ShowInColumnChooser).OrderBy(x => x.Number).ToListAsync(cancellationToken: cancellationToken);
        return ApiResponseFactory<List<ComponentProperty>>.Ok(componentSetting);
    }
}