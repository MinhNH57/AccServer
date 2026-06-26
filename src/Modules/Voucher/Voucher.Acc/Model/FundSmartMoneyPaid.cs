using System.ComponentModel.DataAnnotations;

namespace Voucher.Acc.Model;

public class FundSmartMoneyPaid
{
    public Guid IdVouchers {get;set;}
    public string? FundingSourceCode {get;set;}
    public DateTime? RecordDate {get;set;}
    public decimal? Amount {get;set;}
    [Key]
    public Guid Id {get;set;}
    public int IdAsc {get;set;}
    public int? CodeUnit {get;set;}
    public string? Notes {get;set;}
    public bool IsActive { get; set; } = true;
}
