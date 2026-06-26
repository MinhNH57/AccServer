using System;

namespace Ledger.API.Model;

public class GetFAVoucherReferenceQuery
{
    public int RefType { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int Times { get; set; }
    public Guid? FixedAssetId { get; set; }
    public string ListAccount { get; set; }
}
