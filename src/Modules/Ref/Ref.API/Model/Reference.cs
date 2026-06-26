using System;

namespace Ref.API.Model;

public class Reference : IBaseEntity
{
    public Guid RefId1 { get; set; }
    public Guid RefId2 { get; set; }
    public Guid ReferenceId { get; set; }
    public int RefType1 { get; set; }
    public int RefType2 { get; set; }
    public int ReferenceType { get; set; }
    public int SortOrder { get; set; }
    public string RefNoFinance2 { get; set; }
    public string RefNoManagement2 { get; set; }
    public double? TotalAmountOc { get; set; }
    public double? Amount { get; set; }
    public int State { get; set; } = 0;
}
