using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using SmartAccCloud.Application.Commons.Enums;
using SmartAccCloud.Application.Pagination;

namespace SmartAccCloud.Application.Extensions;

public static class QueryableExtension
{
    public static async Task<Pagination.PagedResult<T>> PaginateAsync<T>(this IQueryable<T> queryable,
         PaginationRequest request, CancellationToken token)
         where T : class
    {
        var finalQuery = queryable;
        if (request.SearchByFields is { Count: > 0 })
        {
            foreach (var searchFields in request.SearchByFields)
            {
                finalQuery = finalQuery.SearchByContain(searchFields);
            }
        }

        if (request.SortModels is { Count: > 0 })
        {
            finalQuery = finalQuery.OrderByField(request.SortModels.ToList());
        }

        var totalRow = await finalQuery.CountAsync(token).ConfigureAwait(false);
        IQueryable<T> paginationResult;
        if (request.AggregateModels is { Count: > 0 })
        {
            var aggregateResult = await finalQuery.AggregateByField(request.AggregateModels).ConfigureAwait(false);

            paginationResult = finalQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

            return new Pagination.PagedResult<T>()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecode = totalRow,
                AggregateResults = new List<AggregateResult>
                {
                    new() { Key = aggregateResult?.CountKey ?? string.Empty, Value = aggregateResult?.Count ?? 0 },
                    new() { Key = aggregateResult?.SumKey ?? string.Empty, Value = aggregateResult?.Sum ?? 0 }
                },
                Items = await paginationResult.ToListAsync(token).ConfigureAwait(false)
            };
        }

        paginationResult = finalQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

        return new Pagination.PagedResult<T>()
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalRecode = totalRow,
            Items = await paginationResult.ToListAsync(token).ConfigureAwait(false)
        };
    }


    public static IQueryable<T> SearchByContain<T>(this IQueryable<T> source, SearchModel searchModel)
    {
        if (string.IsNullOrEmpty(searchModel.SearchValue)) return source;

        var parameter = Expression.Parameter(typeof(T), "item");
        var memberSelector = searchModel.SearchFieldName.Split('.')
            .Aggregate((Expression)parameter, Expression.PropertyOrField);

        var memberType = memberSelector.Type;
        var value = searchModel.SearchValue;

        if (value != null && value.GetType() != memberType)
        {
            //value = (string)Convert.ChangeType(value, memberType);
            var valueChangeType = Convert.ChangeType(value, memberType);
            var constainExpressionValue = Expression.Constant(valueChangeType, memberType);
            var expression = Expression.Equal(memberSelector, constainExpressionValue);
            var predicate = Expression.Lambda<Func<T, bool>>(expression, parameter);

            return source.Where(predicate);
        }
        else
        {
            var constainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            if (constainsMethod == null) return source;

            var constainExpressionValue = Expression.Constant(value, typeof(string));
            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
            if (toLowerMethod == null) return source;

            var toLowerLeftSideExp = Expression.Call(memberSelector, toLowerMethod);
            var toLowerRightSideExp = Expression.Call(constainExpressionValue, toLowerMethod);
            var expression = Expression.Call(toLowerLeftSideExp, constainsMethod, toLowerRightSideExp);
            var predicate = Expression.Lambda<Func<T, bool>>(expression, parameter);

            return source.Where(predicate);
        }
    }

    public static IQueryable<T> OrderByField<T>(this IQueryable<T> source, List<SortModel> models)
    {
        //OrderBy(fields1 asc, fields2 desc, ...)

        string orderingQuery = string.Empty;
        for (int i = 0; i < models.Count; i++)
        {
            if (i == models.Count - 1)
            {
                orderingQuery = models[i].SortField + " " + models[i].SortDirection;
                break;
            }
            orderingQuery = models[i].SortField + " " + models[i].SortDirection + ", ";
        }

        return source.OrderBy(orderingQuery);
    }

    public static async Task<dynamic?> AggregateByField<T>(this IQueryable<T> source, IDictionary<string, AggregateFunc> aggregateModels)
    {
        var fieldSum = aggregateModels.FirstOrDefault(c => c.Value == AggregateFunc.Sum).Key;
        var fieldCount = aggregateModels.FirstOrDefault(c => c.Value == AggregateFunc.Count).Key;

        var lambdaSumExpression = DynamicExpressionParser.ParseLambda<T, decimal>(new ParsingConfig(), true, $"c => Convert.ToDecimal(c.{fieldSum})");

        var aggregateResult = await source
            .GroupBy(c => true).Select(c => new
            {
                CountKey = $"{fieldCount} - count",
                Count = c.Count(),
                SumKey = $"{fieldSum} - sum",
                Sum = c.AsQueryable().Sum(lambdaSumExpression),
            })
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        return aggregateResult;
    }

}