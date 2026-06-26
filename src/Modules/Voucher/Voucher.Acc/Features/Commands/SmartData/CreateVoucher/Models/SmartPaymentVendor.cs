using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voucher.Acc.Features.Commands.SmartData.CreateVoucher.Models;
public class SmartPaymentVendor
{
    public Guid IdSource { get; set; }
    public Guid IdContents { get; set; }
    public DateTime? RecordDate { get; set; }
    public string? NumberOfVouchers { get; set; }
    public DateTime? VoucherDate { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? Description { get; set; }
    public DateTime? MaturityDate { get; set; }
    public decimal? AmountDebt { get; set; }
    public decimal? AmountOwed { get; set; }
    public decimal? AmountPaid { get; set; }
    public double? DiscountRate { get; set; }
    public decimal? AmountDiscount { get; set; }
    public string? Status { get; set; }
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? Modified { get; set; }
    public string? ModifiedBy { get; set; }
    public string? AccountsPayable { get; set; }
    public decimal? AmountOfMoney { get; set; }
    public double? ExchangeRate { get; set; }
    public double? AmountOfMoneyUsd { get; set; }
    public string? ForeignCurrencyType { get; set; }
    public string? ContractCode { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? AccountSymbol { get; set; }
    public double? PriceForeignCurrencyDebt { get; set; }
    public double? PriceForeignCurrencyOwed { get; set; }
    public double? PriceForeignCurrencyPaid { get; set; }
}
