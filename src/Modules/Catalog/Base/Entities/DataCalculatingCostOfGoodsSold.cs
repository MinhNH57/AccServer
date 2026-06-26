using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Base.Entities;
public class DataCalculatingCostOfGoodsSold
{
    public int Id { get; set; }
    public string? UserCode { get; set; }
    public string? DataType { get; set; }
    public string? Parameter { get; set; }
    public int? CodeUnit { get; set; }
    public int? Ordinal { get; set; }
    public string? NumberOfVouchers { get; set; }
    public DateTime? RecordDate { get; set; }
    public DateTime? VoucherDate { get; set; }
    public string? DebitSide { get; set; }
    public string? CreditSide { get; set; }
    public string? AccountSymbol { get; set; }
    public string? WarehoseCode { get; set; }
    public string? WarehoseName { get; set; }
    public string? CommodityCode { get; set; }
    public string? CommodityName { get; set; }
    public string? UnitPcs { get; set; }
    public double? Price { get; set; }
    public double? Quantity { get; set; }
    public decimal? AmountOfMoney { get; set; }
    public double? Price15 { get; set; }
    public double? Quantity15 { get; set; }
    public decimal? AmountOfMoney15 { get; set; }
    public string? Description { get; set; }
    public string? Time { get; set; }
}
