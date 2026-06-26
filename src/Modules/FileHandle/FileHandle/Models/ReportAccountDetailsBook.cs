using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileHandle.Models;

public class ReportAccountDetailsBook
{
    public int Id { get; set; }

    public int? NumericalOrderPrint { get; set; }
    public int? NumericalOrder { get; set; }
    public int? MaxNumericalOrder { get; set; }

    [MaxLength(20)]
    public string? SmartSort { get; set; }

    [MaxLength(20)]
    public string? UserCode { get; set; }

    [MaxLength(50)]
    public string? Parameter { get; set; }

    public int? CodeUnit { get; set; }

    [MaxLength(250)]
    public string? NameUnit { get; set; }

    [MaxLength(50)]
    public string? DataType { get; set; }

    public Guid? IdVoucher { get; set; }

    [MaxLength(250)]
    public string? ForeignCurrencyType { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal? ExchangeRate { get; set; }

    public DateTime? RecordDate { get; set; }   // smalldatetime -> DateTime?
    public DateTime? VoucherDate { get; set; }  // smalldatetime -> DateTime?

    public bool? NoExcel { get; set; }

    [MaxLength(50)]
    public string? TaxCode { get; set; }

    [MaxLength(50)]
    public string? DayBefore { get; set; }

    [MaxLength(50)]
    public string? NextDay { get; set; }

    [MaxLength(10)]
    public string? EndDate { get; set; }

    [MaxLength(10)]
    public string? BeginDate { get; set; }

    [MaxLength(10)]
    public string? Date { get; set; }

    [MaxLength(50)]
    public string? NumberOfVouchers { get; set; }

    [MaxLength(50)]
    public string? InvoiceNumber { get; set; }

    [MaxLength(512)]
    public string? Description { get; set; }

    [MaxLength(100)]
    public string? Note { get; set; }
    [MaxLength(100)]
    public string? Note1 { get; set; }
    [MaxLength(100)]
    public string? Note2 { get; set; }
    [MaxLength(100)]
    public string? Note3 { get; set; }
    [MaxLength(100)]
    public string? Note4 { get; set; }
    [MaxLength(100)]
    public string? Note5 { get; set; }

    [MaxLength(50)]
    public string? GroupCode { get; set; }

    [MaxLength(250)]
    public string? GroupName { get; set; }

    [MaxLength(50)]
    public string? ObjectCode { get; set; }

    [MaxLength(250)]
    public string? ObjectName { get; set; }

    [MaxLength(250)]
    public string? Email { get; set; }

    [MaxLength(250)]
    public string? Address { get; set; }

    [MaxLength(50)]
    public string? CommodityCode { get; set; }

    [MaxLength(255)]
    public string? CommodityName { get; set; }

    [MaxLength(50)]
    public string? UnitPcs { get; set; }

    // SQL float -> C# double
    public double? Price { get; set; }
    public double? Quantity { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    public decimal? AmountOfMoney { get; set; }

    [MaxLength(20)]
    public string? DebitSide { get; set; }

    [MaxLength(20)]
    public string? CreditSide { get; set; }

    [MaxLength(20)]
    public string? AccountSymbol { get; set; }

    [MaxLength(250)]
    public string? AccountName { get; set; }

    public int? AccountLevel { get; set; }

    public decimal? DebitBalance { get; set; } // Hạn mức dư nợ

    public decimal? DebtBalancBegin { get; set; } // Dư đầu nợ

    public decimal? CreditBalancBegin { get; set; } // Dư đầu có

    public decimal? DebtArise { get; set; } // Phát sinh nợ

    public decimal? CreditArise { get; set; } // Phát sinh có

    public decimal? DebtBalancRemaining { get; set; } // Dư cuối nợ còn

    public decimal? CreditBalancRemaining { get; set; } // Dư cuối có còn

    public decimal? DebtBalancEnd { get; set; } // Dư cuối nợ

    public decimal? CreditBalancEnd { get; set; } // Dư cuối có

    public decimal? DebitBalanceUsd { get; set; }

    public decimal? DebtBalancBeginUsd { get; set; }

    public decimal? CreditBalancBeginUsd { get; set; }

    public decimal? DebtAriseUsd { get; set; }

    public decimal? CreditAriseUsd { get; set; }

    public decimal? DebtBalancRemainingUsd { get; set; }

    public decimal? CreditBalancRemainingUsd { get; set; }

    public decimal? DebtBalancEndUsd { get; set; }

    public decimal? CreditBalancEndUsd { get; set; }

    [MaxLength(50)]
    public string? DayText { get; set; }

    [MaxLength(250)]
    public string? AmountInWords { get; set; }

    [MaxLength(250)]
    public string? Headline { get; set; }

    [MaxLength(250)]
    public string? Template { get; set; } // Tài khoản đối ứng

    [MaxLength(250)]
    public string? Decision { get; set; }

    [MaxLength(250)]
    public string? Time { get; set; }
}