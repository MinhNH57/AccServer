using System.Data;
using BuildingBlocks.Dapper;

namespace Report.Features.Mobile.Fund.FundSavingsSummaryBook;

public class FundSavingsSummaryBookStore : StoredProcedureBase
{
    public const string Parameter = "@Parameter";
    public const string Id = "@Id";
    public const string UserCode = "@UserCode";
    public const string CodeUnit = "@CodeUnit";
    public const string AccountSymbol = "@AccountSymbol";
    public const string BeginDate = "@BeginDate";
    public const string EndDate = "@EndDate";
    public const string Date = "@Date";
    public const string PathImages = "@PathImages";
    public const string PathLogo = "@PathLogo";
    public const string WareHouseCode = "@WareHouseCode";
    public const string ProductCode = "@ProductCode";
    public const string SmartSoftware = "@SmartSoftware";


    public FundSavingsSummaryBookStore(
        string parameter, 
        string id = "", 
        string userCode = "", 
        int codeUnit =0, 
        string accountSymbol = "", 
        string beginDate = "", 
        string endDate = "", 
        string date = "", 
        string pathImages = "", 
        string pathLogo = "", 
        string wareHouseCode = "", 
        string productCode = "", 
        string smartSoftware = ""
    )
        : base("FundSavingsSummaryBookBK")
    {
        Parameters.Add(Parameter, parameter, DbType.String);
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
    }
}