using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Base.Entities;
public class ReportCalculateProductionCost
{
    public int Id { get; set; }
    public string? UserCode { get; set; }
    public string? Parameter { get; set; }
    public int? CodeUnit { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public double? Quantity { get; set; }
    public double? Price { get; set; }
    public decimal? Amount { get; set; }
    public decimal? Amount621 { get; set; }
    public decimal? Amount622 { get; set; }
    public decimal? Amount627 { get; set; }
    public decimal? Amount154 { get; set; }
    public string? WarehoseCode { get; set; }
    public string? WarehoseName { get; set; }
    public string? ContractCode { get; set; }
    public string? ContractName { get; set; }
    public string? ProjectCode { get; set; }
    public string? ProjectName { get; set; }
    public string? ConstructionCode { get; set; }
    public string? ConstructionName { get; set; }
    public string? Notes { get; set; }
    public double? AccountRatio627 { get; set; }
    public double? AccountRatio622 { get; set; }
}
