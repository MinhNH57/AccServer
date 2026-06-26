using System.Data;
using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using Dapper;
using Systems.Infrastructure;

namespace Systems.Features.Cms;

public record FindColumnNamesQuery(string TableId) : IQuery<ApiResponse<string>>;

public class FindColumnNamesQueryHandler (SystemDbcontext dbContext) : IQueryHandler<FindColumnNamesQuery, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> Handle(FindColumnNamesQuery query, CancellationToken cancellationToken)
    {
        var connection = dbContext.Database.GetDbConnection();
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync(cancellationToken);
        }

        var columnNames = await connection.QueryFirstAsync<string>($"SELECT STRING_AGG(COLUMN_NAME, ',') FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @Id", new { Id = query.TableId });

        return ApiResponseFactory<string>.Ok(columnNames);
    }
}