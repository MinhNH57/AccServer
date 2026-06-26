using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using BuildingBlocks.Web;
using System.Linq.Dynamic.Core;
using Microsoft.Data.SqlClient;
using Systems.Infrastructure;
using Systems.Infrastructure.Entities;

namespace Systems.Features.Cms;

public record FindComponentSettingQuery(FilteringRequest Filtering, ICurrentUser CurrentUser) : ICacheQuery<Result<List<ComponentSetting>>>
{
    public string CacheKey => $"KT:{CurrentUser.TenantId}:setting:{string.Join("", Filtering.Ids)}";
    public string TypeName => "";
    public TimeSpan Expiration => TimeSpan.FromHours(3);
}

public class FindComponentSettingQueryHandler(SystemDbcontext dbContext) : IQueryHandler<FindComponentSettingQuery, Result<List<ComponentSetting>>>
{
    public async Task<Result<List<ComponentSetting>>> Handle(FindComponentSettingQuery request, CancellationToken cancellationToken)
    {
        try
        {
            string stringQuery = string.Empty;

            var root = (IQueryable<ComponentSetting>)dbContext.ComponentSettings
                .Where(c => !string.IsNullOrEmpty(c.ComponentCode))
                .Include(x => x.ComponentProperties.OrderBy(y => y.Number));
            //.Include(x => x.ContextMenuProperties.OrderBy(y => y.Number));

            if (!string.IsNullOrEmpty(request.Filtering.Ids))
            {
                //root = root.Where(x => request.Filtering.Ids.Contains(x.Id.ToString()) || request.Filtering.Ids.Contains(x.ComponentCode));
                root = root.Where(x => request.Filtering.Ids.Contains(x.ComponentCode));
                stringQuery = string.Join("&", $"ids={request.Filtering.Ids}");
            }

            if (!string.IsNullOrEmpty(request.Filtering.Search))
            {
                root = root.Where(x => x.ComponentName.Contains(request.Filtering.Search));
                stringQuery = string.Join("&", $"search={request.Filtering.Search}");
            }

            if (request.Filtering.Filter.Length > 0)
            {
                var filterClauses = string.Empty;
                foreach (var filter in request.Filtering.Filter)
                {
                    var filterSplit = filter.Split(":");
                    if (filterSplit.Length == 3)
                    {
                        filterClauses += $"{filterSplit[0]} == \"{filterSplit[2]}\" && ";
                    }
                    else if (filterSplit.Length == 2)
                    {
                        filterClauses += $"{filterSplit[0]} == \"{filterSplit[1]}\" && ";
                    }
                    else
                    {
                        throw new Exception("Điều kiện lọc không hợp lệ.");
                    }
                }

                filterClauses = filterClauses[..^4];

                root = root.Where(filterClauses);
            }

            if (!string.IsNullOrEmpty(request.Filtering.OwnerId))
            {
                root = root.Where(x => x.OwnerId == request.Filtering.OwnerId);
                stringQuery = stringQuery + $"&owner_id={request.Filtering.OwnerId}";
            }

            if (!string.IsNullOrEmpty(request.Filtering.Status))
            {
                root = root.Where(x => x.Status == request.Filtering.Status);
            }

            if (!string.IsNullOrEmpty(request.Filtering.Fields))
            {
                var fields = $"new {request.Filtering.Fields}";
                root = (IQueryable<ComponentSetting>)root.Select(fields);
            }

            var componentSettings = await root.ToListAsync(cancellationToken: cancellationToken);

            return Result.Success(componentSettings);
        }
        catch (SqlException ex)
        {
            if (ex.Message.Contains("Invalid column name"))
            {
                // Extract tên cột bị thiếu từ message lỗi
                var missingColumns = new List<string>();
                var lines = ex.Message.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    if (line.Contains("Invalid column name"))
                    {
                        // Ví dụ message: Invalid column name 'AllowGroup'.
                        var start = line.IndexOf('\'') + 1;
                        var end = line.LastIndexOf('\'');
                        if (start > 0 && end > start)
                        {
                            var colName = line.Substring(start, end - start);
                            missingColumns.Add(colName);
                        }
                    }
                }

                var missingColsStr = string.Join(", ", missingColumns);
                // Bạn có thể log hoặc trả về lỗi rõ ràng
                var errorMessage = $"Thiếu cột trong bảng ComponentSettings: {missingColsStr}";

                // Trả về lỗi hoặc throw lại exception với thông tin chi tiết
                //return Result.Failure<List<ComponentSetting>>(errorMessage);
            }

            // Nếu lỗi khác thì throw lại
            throw;
        }

    }
}