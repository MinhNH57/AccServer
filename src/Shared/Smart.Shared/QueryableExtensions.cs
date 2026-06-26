using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace Smart.Shared;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, List<Filter> filters)
    {
        if (filters == null || filters.Count == 0)
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression? combined = null;

        foreach (var f in filters)
        {
            var propertyName = Enum.GetName(typeof(EnumFields), f.Property);
            if (string.IsNullOrEmpty(propertyName))
                continue;

            var property = Expression.Property(parameter, propertyName);
            Expression? comparison = BuildComparison(property, f);

            if (comparison == null)
                continue;

            combined = combined == null
                ? comparison
                : f.Operand == Operand.Or
                    ? Expression.OrElse(combined, comparison)
                    : Expression.AndAlso(combined, comparison);
        }

        if (combined == null)
            return query;

        var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
        return query.Where(lambda);
    }

    private static Expression? BuildComparison(Expression property, Filter f)
    {
        var op = f.Operator;
        var type = f.DataType;

        // ✅ IS NULL / IS NOT NULL
        if (op == Operator.IsNull)
            return Expression.Equal(property, Expression.Constant(null));
        if (op == Operator.IsNotNull)
            return Expression.NotEqual(property, Expression.Constant(null));

        // ✅ CONVERT VALUE
        object? convertedValue = ConvertValue(f.Value, type);
        if (convertedValue == null && op is not Operator.IsNull and not Operator.IsNotNull)
            return null;

        Expression constant = Expression.Constant(convertedValue, property.Type);

        // ✅ Handle Array / Within / Notin
        if (op == Operator.Within || op == Operator.Notin)
        {
            var list = ConvertToList(f.Value, type);
            var containsMethod = typeof(Enumerable)
                .GetMethods()
                .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                .MakeGenericMethod(property.Type);
            var listConst = Expression.Constant(list);
            var call = Expression.Call(containsMethod, listConst, property);
            return op == Operator.Within ? call : Expression.Not(call);
        }

        // ✅ Normal comparisons
        return op switch
        {
            Operator.Equals => Expression.Equal(property, constant),
            Operator.NotEquals => Expression.NotEqual(property, constant),
            Operator.GreaterThan => Expression.GreaterThan(property, constant),
            Operator.GreaterThanEquals => Expression.GreaterThanOrEqual(property, constant),
            Operator.LessThan => Expression.LessThan(property, constant),
            Operator.LessThanEquals => Expression.LessThanOrEqual(property, constant),

            Operator.Contains => Expression.Call(property, nameof(string.Contains), null, constant),
            Operator.NotContains => Expression.Not(Expression.Call(property, nameof(string.Contains), null, constant)),
            Operator.StartsWith => Expression.Call(property, nameof(string.StartsWith), null, constant),
            Operator.EndsWith => Expression.Call(property, nameof(string.EndsWith), null, constant),

            Operator.ContainsWords => Expression.Call(property, nameof(string.Contains), null, constant),
            Operator.NotContainsWords => Expression.Not(Expression.Call(property, nameof(string.Contains), null, constant)),

            _ => null
        };
    }

    private static object? ConvertValue(string value, DataType type)
    {
        try
        {
            return type switch
            {
                DataType.String => value,
                DataType.Boolean => bool.TryParse(value, out var b) ? b : null,
                DataType.DateTime or DataType.DateTimeFull => DateTime.TryParse(value, out var dt) ? dt : null,
                DataType.Number => int.TryParse(value, out var i) ? i :
                                   double.TryParse(value, out var d) ? d : null,
                DataType.Decimal => decimal.TryParse(value, out var dec) ? dec : null,
                DataType.Guid => Guid.TryParse(value, out var g) ? g : null,
                DataType.ArrayNumber => value.Split(',').Select(s => Convert.ToDouble(s.Trim())).ToList(),
                _ => value
            };
        }
        catch
        {
            return null;
        }
    }

    private static IEnumerable<object> ConvertToList(string value, DataType type)
    {
        var items = value.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in items)
        {
            var converted = ConvertValue(item.Trim(), type);
            if (converted != null)
                yield return converted;
        }
    }

    public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, List<Sort> sorts)
    {
        if (sorts == null || sorts.Count == 0)
            return query;

        IOrderedQueryable<T>? ordered = null;
        var parameter = Expression.Parameter(typeof(T), "x");

        foreach (var s in sorts)
        {
            var propertyName = Enum.GetName(typeof(EnumFields), s.Property);
            if (string.IsNullOrEmpty(propertyName))
                continue;

            var property = Expression.PropertyOrField(parameter, propertyName);
            var keySelector = Expression.Lambda(property, parameter);

            string methodName;

            if (ordered == null)
                methodName = s.Desc ? "OrderByDescending" : "OrderBy";
            else
                methodName = s.Desc ? "ThenByDescending" : "ThenBy";

            var method = typeof(Queryable).GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type);

            query = (IOrderedQueryable<T>)method.Invoke(null, [query, keySelector])!;
            ordered = (IOrderedQueryable<T>)query;
        }

        return query;
    }

    public static async Task<Dictionary<string, object?>> GetSummaryAsync<T>(
        this DbContext context,
        string queryString,
        List<int> summaryColumns)
        where T : class
    {
        if (summaryColumns == null || summaryColumns.Count == 0)
            return [];

        // ✅ Lấy tên bảng thực từ EF metadata
        var entityType = context.Model.FindEntityType(typeof(T))!;
        var tableName = entityType.GetTableName()!;
        var schema = entityType.GetSchema();
        var fullTableName = string.IsNullOrEmpty(schema)
            ? $"[{tableName}]"
            : $"[{schema}].[{tableName}]";

        // ✅ Build SELECT SUM(...) AS [Column] dựa theo EnumFields
        var sb = new StringBuilder("SELECT ");
        var sumCols = new List<string>();

        foreach (var colId in summaryColumns)
        {
            var propertyName = Enum.GetName(typeof(EnumFields), colId);
            if (string.IsNullOrEmpty(propertyName))
                continue;

            var prop = entityType.FindProperty(propertyName);
            if (prop == null)
                continue;

            // ✅ Xác định kiểu dữ liệu
            var clrType = Nullable.GetUnderlyingType(prop.ClrType) ?? prop.ClrType;
            bool isNumeric = clrType == typeof(int) ||
                             clrType == typeof(long) ||
                             clrType == typeof(short) ||
                             clrType == typeof(float) ||
                             clrType == typeof(double) ||
                             clrType == typeof(decimal) ||
                             clrType == typeof(byte);

            string aggFunc = isNumeric ? "SUM" : "COUNT";

            sumCols.Add($"{aggFunc}([{propertyName}]) AS [{propertyName}]");
        }

        if (sumCols.Count == 0)
            return [];

        sb.Append(string.Join(", ", sumCols));
        if (string.IsNullOrWhiteSpace(queryString))
        {
            sb.Append($" FROM {fullTableName}");
        }
        else
        {
            sb.Append(" FROM (");
            sb.Append(queryString);
            sb.Append(") AS QueryTable");
        }

        var sql = sb.ToString();

        // ✅ Thực thi SQL (ADO thuần, 1 query duy nhất)
        await using var conn = context.Database.GetDbConnection();
        if (conn.State != ConnectionState.Open)
            await conn.OpenAsync();

        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        var result = new Dictionary<string, object?>();
        await using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                result[reader.GetName(i)] =
                    reader.IsDBNull(i) ? null : Convert.ToDouble(reader.GetValue(i));
            }
        }

        return result;
    }
}
