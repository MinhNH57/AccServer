using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Base.Entities;
public class DataAdminAccountTransfer
{
    public int Id { get; set; }
    public string? UserCode { get; set; }
    public string? Parameter { get; set; }
    public int? CodeUnit { get; set; }
    public int? Ordinal { get; set; }
    public string? NumberOfVouchers { get; set; }
    public DateTime? RecordDate { get; set; }
    public string? AccountSymbol { get; set; }
    public string? DebitSide { get; set; }
    public string? CreditSide { get; set; }
    public string? AccountName { get; set; }
    public decimal? AmountOfMoney { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? ProjectCode { get; set; }
    public string? ProjectName { get; set; }
    public string? ProductionActivitieCode { get; set; }
    public string? ProductionActivitieName { get; set; }
    public string? RevenueExpenseCode { get; set; }
    public string? RevenueExpenseName { get; set; }
    public string? WarehoseCode { get; set; }
    public string? WarehoseName { get; set; }
    public string? DebitObjectCode { get; set; }
    public string? DebitObjectName { get; set; }
    public string? CreditObjectCode { get; set; }
    public string? CreditObjectName { get; set; }
    public string? CommodityCode { get; set; }
    public string? CommodityName { get; set; }
    public string? UnitPcs { get; set; }
    public string? Description { get; set; }
    public string? Time {get;set;}
}
