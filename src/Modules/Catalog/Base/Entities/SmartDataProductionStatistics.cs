using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Base.Entities;
public class SmartDataProductionStatistics
{
    public Guid Id { get; set; }
    public string? StationId { get; set; }
    public DateTime? RecordDate { get; set; }
    public string? CommodityCode { get; set; }
    public string? CommodityName { get; set; }
    public string? UnitPcs { get; set; }
    public double? Quantity { get; set; }
    public double? Price { get; set; }
    public decimal? AmountOfMoney { get; set; }
    public double? QuantityNG { get; set; }
    public string? IdData { get; set; }
    public string? UserCode { get; set; }
    public string? StatisticalCode { get; set; }
    public string? StageCode { get; set; }
    public string? StageName { get; set; }
    public string? NumberOfVouchers { get; set; }
    public string? IdVouchers { get; set; }
    public double? QuantityOfInventory { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public string? Notes { get; set; }
    public DateTime CreateDate { get; set; }
    public int IdAsc { get; set; }
    public bool? Approve { get; set; }
    public DateTime? ApproveDate { get; set; }
    public string? Approver { get; set; }
}
