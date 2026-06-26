using System;

namespace Ledger.API.Model;

public class FAVoucherReference
{
    public bool Selected { get; set; }
    public Guid RefId { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime RefDate { get; set; }
    public string RefNo { get; set; }
    public string JournalMemo { get; set; }
    public string CreditAccount { get; set; }
    public string DebitAccount { get; set; }
    public double Amount { get; set; }
    public int RefType { get; set; }
    public int SortOrder { get; set; }
    public Guid RefDetailId { get; set; }
    public int DetailPostOrder { get; set; }
}