using System.ComponentModel.DataAnnotations;

namespace Voucher.Acc.Model;

public class SalesSmartContentsDataTemp
{
    public Guid IdContents {get;set;}
    [Key]
    public int IdAsc { get; set; }
    public string? DataType {get;set;}
    public string? CommodityCode {get;set;}
    public string? CommodityName {get;set;}
    public string? UnitPcs {get;set;}
    public double? Quantity {get;set;}
    public double? Price {get;set;}
    public decimal? AmountOfMoney {get;set;}
    public string? Notes {get;set;}
    public int? CodeUnit {get;set;}
    public bool IsActive {get;set;}
    public DateTime CreateDate {get;set;} =DateTime.Now;
    public string? CreateBy {get;set;}
    public DateTime? ModifyDate {get;set;}
    public string? ModifyBy {get;set;}
}