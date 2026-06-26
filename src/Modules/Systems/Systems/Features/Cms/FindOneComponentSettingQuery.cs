using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using Systems.Infrastructure;
using Systems.Infrastructure.Entities;

namespace Systems.Features.Cms;

public record FindOneComponentSettingQuery(string SettingId) : IQuery<ApiResponse<ComponentSetting>>;


public class FindOneComponentSettingQueryHandler(SystemDbcontext dbContext) : IQueryHandler<FindOneComponentSettingQuery, ApiResponse<ComponentSetting>>
{
    public async Task<ApiResponse<ComponentSetting>> Handle(FindOneComponentSettingQuery query, CancellationToken cancellationToken)
    {
        var componentSetting = await dbContext.ComponentSettings
            .Include(x => x.ComponentProperties
                .OrderBy(a => a.Number))
            .Include(x => x.ContextMenuProperties
                .OrderBy(c => c.Number))
            .FirstOrDefaultAsync(x => x.Id.ToString() == query.SettingId || x.ComponentCode == query.SettingId, cancellationToken: cancellationToken);


        return ApiResponseFactory<ComponentSetting>.Ok(componentSetting!);

    }
}