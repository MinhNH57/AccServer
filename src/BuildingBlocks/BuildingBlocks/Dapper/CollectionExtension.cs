using Cassandra;
using System.Data;
using System.Reflection;

namespace BuildingBlocks.Dapper;

public static class CollectionExtension
{
    public static DataTable ToDataTable<T>(this List<T> items) where T : class
    {
        var dataTable = new DataTable(typeof(T).Name);
        //Get all the properties
        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in props)
        {
            var propType = prop.PropertyType;
            var underlying = Nullable.GetUnderlyingType(propType) ?? propType;
            //Setting column names as Property names

            dataTable.Columns.Add(new DataColumn(prop.Name, underlying)
            {
                AllowDBNull = !underlying.IsValueType || Nullable.GetUnderlyingType(propType) != null
            });
        }

        foreach (var item in items)
        {
            DataRow row = dataTable.NewRow();
            foreach (var p in props)
            {
                var value = p.GetValue(item, null);
                row[p.Name] = value ?? DBNull.Value;
            }
            dataTable.Rows.Add(row);
        }
        return dataTable;
    }
}