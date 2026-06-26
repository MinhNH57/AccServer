using System.ComponentModel.DataAnnotations;

namespace Voucher.Acc.Model;

public class SmartLogsOfUsingVouchers
{
    public Guid IdContents {get;set;}
    [Key]
    public int IdAsc {get;set;}
    public string? SubSystem {get;set;}
    public string? DataType {get;set;}
    public string? UserFuntionName {get;set;}
    //public string? UserFuntionCode {get;set;}
    //public string? FirstData {get;set;}
    //public string? LastData {get;set;}
    //public string? Notes {get;set;}
    //public string? UserName {get;set;}
    //public int? CodeUnit {get;set;}
    //public bool IsActive {get;set;}
    public DateTime CreateDate {get;set;} = DateTime.Now;
    public string? CreateBy {get;set;}
    public DateTime? ModifyDate {get;set;}
    public string? ModifyBy {get;set;}
    public Guid IdData {get;set;}
    public Guid IdSource {get;set;}
}