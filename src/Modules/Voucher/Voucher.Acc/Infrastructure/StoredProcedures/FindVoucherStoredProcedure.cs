using System.Data;
using System.Reflection;
using BuildingBlocks.Dapper;
using BuildingBlocks.Pagination.Version1;

namespace Voucher.Acc.Infrastructure.StoredProcedures;

public class FindVoucherStoredProcedure : StoredProcedureBase
{
    private const string FindGridSettingStoredProcedureName = "FindVoucher";

    private const string CatalogTypeParameterName = "@CatalogType";

    private const string IdsParameterName = "@Ids";

    private const string SearchTermParameterName = "@SearchTerm";

    private const string OwnerIdParameterName = "@OwnerId";

    private const string FieldsParameterName = "@Fields";

    private const string FilterParameterName = "@Filter";
    private const string SortParameterName = "@Sort";
    private const string PageParameterName = "@Page";
    private const string PageSizeParameterName = "@PageSize";

    private const string TypeView = "@ViewType";

    public FindVoucherStoredProcedure(string catalogType, string? ids, string? searchTerm, string? fields,
        List<Filter> filter, List<Sort> sort, int? page, int? pageSize, string? typeView = null, string? codeUser = null)
        : base(FindGridSettingStoredProcedureName)
    {
        Parameters.Add(CatalogTypeParameterName, catalogType, DbType.String);
        Parameters.Add(IdsParameterName, ids, DbType.String);
        Parameters.Add(SearchTermParameterName, searchTerm, DbType.String);
        Parameters.Add(FieldsParameterName, fields, DbType.String);
        Parameters.Add(FilterParameterName, ToDataTable(filter), DbType.Object);
        Parameters.Add(SortParameterName, ToDataTable(sort), DbType.Object);
        Parameters.Add(PageParameterName, page, DbType.Int32);
        Parameters.Add(PageSizeParameterName, pageSize, DbType.Int32);
        Parameters.Add(TypeView, typeView, DbType.String);
    }

    private static DataTable ToDataTable<T>(List<T> items)
    {
        var dataTable = new DataTable(typeof(T).Name);
        //Get all the properties
        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in props)
        {
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name);
        }

        foreach (var item in items)
        {
            var values = new object[props.Length];
            for (var i = 0; i < props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = props[i].GetValue(item, null);
            }

            dataTable.Rows.Add(values);
        }

        //put a breakpoint here and check datatable
        return dataTable;
    }
}
