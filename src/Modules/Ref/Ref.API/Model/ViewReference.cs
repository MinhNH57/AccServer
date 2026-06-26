using System;

namespace Ref.API.Model;

public class ViewReference : IBaseEntity
{
    public Guid? SessionId { get; set; }
    public int RowNum { get; set; }
    public DateTime PostedDate { get; set; }
    public Guid RefId { get; set; }
    public int RefType { get; set; }
    public DateTime RefDate { get; set; }
    public string RefNoFinance { get; set; }
    public string RefNoManagement { get; set; }
    public string JournalMemo { get; set; }
    public string AccountObjectCode { get; set; }
    public string AccountObjectName { get; set; }
    public string DebitAccount { get; set; }
    public string CreditAccount { get; set; }
    public double? TotalAmount { get; set; }
    public string ReferenceType { get; set; }
}