using System.Data;
using BuildingBlocks.Dapper;

namespace Voucher.Acc.Features.Commands.AdminAccountTransfer;

public class AdminAccountTransferStore : StoredProcedureBase
{
    private const string Parameter = "@Parameter";
    private const string UserCode = "@UserCode";
    private const string CodeUnit = "@CodeUnit";
    private const string AccountSymbol = "@AccountSymbol";
    private const string BeginDate = "@BeginDate";
    private const string EndDate = "@EndDate";
    private const string WareHouseCode = "@WareHouseCode";
    private const string ProductCode = "@ProductCode";
    private const string SmartSoftware = "@SmartSoftware";



    public AdminAccountTransferStore(string parameter, string userCode, int codeUnit,string accountSymbol, string beginDate ,string endDate,string wareHouseCode, string productCode, string smartSoftware) : base("AdminAccountTransfer")
    {
        Parameters.Add(Parameter, parameter, DbType.String);
        Parameters.Add("@Id", "", DbType.String);
        Parameters.Add(UserCode, userCode, DbType.String);
        Parameters.Add(CodeUnit, codeUnit, DbType.Int16);
        Parameters.Add(AccountSymbol, accountSymbol, DbType.String);
        Parameters.Add(BeginDate, beginDate, DbType.String);
        Parameters.Add(EndDate, endDate, DbType.String);
        Parameters.Add(WareHouseCode, wareHouseCode, DbType.String);
        Parameters.Add(ProductCode, productCode, DbType.String);
        Parameters.Add(SmartSoftware, smartSoftware, DbType.String);
    }
}