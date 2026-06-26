using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voucher.Acc.Features.Commands.SmartData.CreateVoucher.Models;

namespace Voucher.Acc.Model.DebtOffSet;
public class SmartDebtOffSet
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? DataType { get; set; }
    public string? DataName { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public string? ContractNo { get; set; }
    public string? NumberOfVouchers { get; set; }
    public DateTime? VoucherDate { get; set; } = DateTime.Now;
    public DateTime? RecordDate { get; set; } = DateTime.Now;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; } = DateTime.Now;
    public string? ModifiedBy { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? ForeignCurrencyType { get; set; }
    public double ExchangeRate { get; set; }
    public string? PersonCode { get; set; }
    public string? PersonName { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? ReasonCode { get; set; }
    public string? ReasonName { get; set; }
    public decimal AllocatedAmountOC { get; set; }
    public decimal AllocatedAmount { get; set; }
    public string? GroupCode { get; set; }
    public string? GroupName { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public int CodeUnit { get; set; } = 888;
    public bool SaveTemp { get; set; }
    public List<SmartDebtOffSetContents>? SmartDebtOffSetContents { get; set; } = new List<SmartDebtOffSetContents>();
}
