using BuildingBlocks.Dapper;
using BuildingBlocks.Pagination.Version1;
using System.Data;
using System.Reflection;

namespace Report.Infrastructure.StoredProcedures;

public class ReportStoredProcedure : StoredProcedureBase
{
    private const string StoreName = "FindCatalog";
    private const string ParameterStore = "@Parameter";
    private const string Id = "@Id";
    private const string UserCode = "@UserCode";
    private const string CodeUnit = "@CodeUnit";
    private const string AccountSymbol = "@AccountSymbol";
    private const string BeginDate = "@BeginDate";
    private const string EndDate = "@EndDate";
    private const string Date = "@Date";
    private const string PathImages = "@PathImages";
    private const string PathLogo = "@PathLogo";
    private const string WareHouseCode = "@WareHouseCode";
    private const string ProductCode = "@ProductCode";
    private const string SmartSoftware = "@SmartSoftware";
    private const string FilterStore = "@FilterStore";

    // @FilterStore FilterStore READONLY 
    public ReportStoredProcedure(string parameter, string? id, string? userCode, int? codeUnit, string? accountSymbol, string? beginDate,
        string? endDate, string? date, string pathImages, string pathLogo, string wareHouseCode, string productCode, string? smartSoftware, List<FilterStore>? filter)
        : base(StoreName)
    {
        Parameters.Add(ParameterStore, parameter, DbType.String);
        Parameters.Add(Id, id, DbType.String);
        Parameters.Add(UserCode, userCode, DbType.String);
        Parameters.Add(CodeUnit, codeUnit, DbType.Int16);
        Parameters.Add(AccountSymbol, accountSymbol, DbType.String);
        Parameters.Add(BeginDate, beginDate, DbType.String);
        Parameters.Add(EndDate, endDate, DbType.String);
        Parameters.Add(Date, date, DbType.String);
        Parameters.Add(PathImages, pathImages, DbType.String);
        Parameters.Add(PathLogo, pathLogo, DbType.String);
        Parameters.Add(WareHouseCode, wareHouseCode, DbType.String);
        Parameters.Add(ProductCode, productCode, DbType.String);
        Parameters.Add(SmartSoftware, smartSoftware, DbType.String);
        Parameters.Add(FilterStore, ToDataTable(filter), DbType.Object);
    }
    private static DataTable ToDataTable<T>(List<T>? items)
    {
        var dataTable = new DataTable(typeof(T).Name);
        //Get all the properties
        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in props)
        {
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name);
        }

        if (items is null) return dataTable;
        foreach (var item in items)
        {
            var values = new object?[props.Length];
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
