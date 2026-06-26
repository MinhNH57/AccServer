using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voucher.Acc.Model.DebtOffSet;
public class SmartDebtOffSetContents
{
    public Guid? IdSource { get; set; } = Guid.NewGuid();
    public Guid? IdContents { get; set; } = Guid.NewGuid();
    public Guid? IdData { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public string? DataType { get; set; } = string.Empty;
    public string? InvoiceNumberContents { get; set; } = string.Empty;
    public string? ContractCode { get; set; } = string.Empty;
    public string? ForeignCurrencyType { get; set; } = string.Empty;
    public double ExchangeRate { get; set; }
    public decimal AllocatedAmountOC { get; set; }
    public decimal AllocatedAmount { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public string? CreateBy { get; set; } = string.Empty;
    public DateTime? ModifyDate { get; set; } = DateTime.Now;
    public string? ModifyBy { get; set; } = string.Empty;
    public string? Notes { get; set; } = string.Empty;
    public int CodeUnit { get; set; } = 888;
}
