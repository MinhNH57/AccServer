using System.ComponentModel.DataAnnotations;

namespace Voucher.Acc.Model;

public class SmartContentsQuanlityControl
{
    public Guid IdContents {get;set;}
    [Key]
    public int IdAsc {get;set;}
    public string? TargetCode {get;set;}
    public string? TargetName {get;set;}
    public string? QCTargetText {get;set;}
    public double? QCTargetNumberBegin {get;set;}
    public double? QCTargetNumberEnd {get;set;}
    public string? CurentValueText {get;set;}
    public double? CurentValueNumber {get;set;}
    public bool IsOk {get;set;}
    public string? Notes {get;set;}
    public int? CodeUnit {get;set;}
    public string? ProductCode {get;set;}
    public string? ProductName {get;set;}
}