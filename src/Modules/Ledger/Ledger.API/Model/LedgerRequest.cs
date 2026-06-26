using System;

namespace Ledger.API.Model;

public class LedgerRequest
{
    public Guid RefId { get; set; }
    public int RefType { get; set; }
    public int EditVersion { get; set; }
    public Guid? BranchId { get; set; }
    public string TableName { get; set; }
    public bool AllowOverOutwardStock { get; set; }
    public bool IsPostAfterSave { get; set; }
}
