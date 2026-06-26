using System.Data;
using BuildingBlocks.Dapper;

namespace Catalog.Fund.Infrastructure.StoredProcedures;

public class GetInfoMemmberStoreProcedure : StoredProcedureBase
{
    private const string FindGridSettingStoredProcedureName = "FindCatalog";
    private const string Parameter = "@Parameter";
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


    public GetInfoMemmberStoreProcedure(string parameter, string? id, string? userCode, int? codeUnit, string? accountSymbol, DateTime? beginDate,
        DateTime? endDate, DateTime? date, string pathImages, string pathLogo , string wareHouseCode, string productCode,string? smartSoftware) 
        : base(FindGridSettingStoredProcedureName)
    {
        Parameters.Add(Parameter, parameter, DbType.String);
        Parameters.Add(Id, id, DbType.String);
        Parameters.Add(UserCode, userCode, DbType.String);
        Parameters.Add(CodeUnit, codeUnit, DbType.Int16);
        Parameters.Add(AccountSymbol, accountSymbol, DbType.String);
        Parameters.Add(BeginDate, beginDate, DbType.Date);
        Parameters.Add(EndDate, endDate, DbType.Date);
        Parameters.Add(Date, date, DbType.Date);
        Parameters.Add(PathImages, pathImages, DbType.String);
        Parameters.Add(PathLogo, pathLogo, DbType.String);
        Parameters.Add(WareHouseCode, wareHouseCode, DbType.String);
        Parameters.Add(ProductCode, productCode, DbType.String);
        Parameters.Add(SmartSoftware, smartSoftware, DbType.String);
    }
}
